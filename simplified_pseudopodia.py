"""
A simplified model of Dictyostelium movement based on software by Mr. Vafadar.
"""
__author__ = 'Ata Kalirad'

__version__ = '1.0'

import os
import pickle
from copy import *
from itertools import *

import numpy as np
import random as rnd
import pandas as pd

class Species(object):
    """The class representing a chemical species. 
    
    """

    def __init__(self, init_n):
        self.n = init_n

    def set_n(self, n):
        self.n = n

    def increase(self):
        self.n += 1
        
    def decrease(self):
        if self.n !=0:
            self.n -= 1

class Voxel(object):

    def __init__(self, RAS, PI3K, PTEN, P2, PI3K_P2, P3, PTEN_P3, PIP, actin, myosin, k_list):
        """Initialize the voxel object, which contains all the reaction for a voxel.
        
        Arguments:
            RAS {int} -- The initial number of molecules in a voxel.
            PI3K {int} -- ...
            PTEN {int} -- ...
            P2 {int} -- ...
            PI3K_P2 {int} -- ...
            P3 {int} -- ...
            PTEN_P3 {int} -- ...
            PIP {int} -- ...
            actin {int} -- ...
            myosin {int} -- ...
            k_list {list} -- The list of reaction constants in order.
        """

        assert len(k_list) == 15
        self.k = k_list
        self.n_reactions = len(k_list)
        self.init_n_RAS = RAS
        self.init_n_PI3K = PI3K
        self.init_n_PTEN = PTEN
        self.init_n_P2 = P2
        self.init_n_PI3K_P2 = PI3K_P2 
        self.init_n_P3 = P3 
        self.init_n_PTEN_P3 = PTEN_P3 
        self.init_n_PIP = PIP
        self.init_n_actin = actin 
        self.init_n_myosin = myosin
        self.clock = 0

    def set_loc(self, loc):
        """Set the location of the voxel in the cell
        
        Arguments:
            loc {tuple} -- A tuple that contains X and y for the voxel.
        """

        self.loc = loc

    def init_species_num(self):
        """Draw the number of chemical species in the voxel from a Poisson dist.
        """

        self.RAS = Species(np.random.poisson(self.init_n_RAS))
        self.PI3K = Species(np.random.poisson(self.init_n_PI3K))
        self.PTEN = Species(np.random.poisson(self.init_n_PTEN))
        self.P2 = Species(np.random.poisson(self.init_n_P2))
        self.PI3K_P2 = Species(np.random.poisson(self.init_n_PI3K_P2)) 
        self.P3 = Species(np.random.poisson(self.init_n_P3)) 
        self.PTEN_P3 = Species(np.random.poisson(self.init_n_PTEN_P3)) 
        self.PIP = Species(np.random.poisson(self.init_n_PIP)) 
        self.actin = Species(np.random.poisson(self.init_n_actin)) 
        self.myosin = Species(np.random.poisson(self.init_n_myosin))
        self.sink = Species(0.)
        self.init_reactions()
        
    def init_reactions(self):
        """Initialize the set of 15 reactions for a voxel.

        The keys refer to specific reactions, and the reactants and products are 
        kept in distinct dictionaries.
        """

        self.reactants = {0: [self.RAS],\
                          1: [self.RAS],\
                          2: [self.PI3K, self.P2],\
                          3: [self.PI3K_P2],\
                          4: [self.PI3K_P2],\
                          5: [self.PTEN, self.P3],\
                          6: [self.PTEN_P3],\
                          7: [self.PTEN_P3],\
                          8: [self.P3],\
                          9: [self.PIP],\
                          10:[self.P2],\
                          11:[self.P3],\
                          12:[self.PTEN],\
                          13:[self.actin],\
                          14:[self.myosin]}
        
        self.products = {0: [self.PI3K],\
                         1: [self.PTEN],\
                         2: [self.PI3K_P2],\
                         3: [self.PI3K, self.P2],
                         4: [self.P3],\
                         5: [self.PTEN_P3],\
                         6: [self.PTEN, self.P3],\
                         7: [self.P2],\
                         8: [self.PIP],\
                         9: [self.P2],\
                         10:[self.PIP],\
                         11:[self.actin],\
                         12:[self.myosin],\
                         13:[self.sink],\
                         14:[self.sink]}

    def get_propensity(self, reactants, k):
        """Calculate propensity
        
        Arguments:
            reactants {list} -- The list of reactants
            k {float} -- the reaction constant
        
        Returns:
            float -- propensity
        """

        prop = k
        for i in reactants:
            prop *= i.n
        return prop
    
    def next_reaction_method(self):
        """Use Gillespie's next reaction method to pick a reaction.
        
        Returns:
            float, float
        """

        #step1: calculate propensities.
        props = np.zeros(self.n_reactions)
        for i in range(len(props)):
            props[i] = self.get_propensity(self.reactants[i], self.k[i])
        #step2: get the reaction times
        times = []
        for i in props:
            if i > 0:
                times.append(1./i*(np.log(1./(rnd.random()))))
            else:
                times.append(np.inf)
        #step3: get smallest time and the index of the reaction with the smallest time
        min_time = np.min(times)
        min_time_ind = np.argmin(times)
        return min_time, min_time_ind

    def execute_reaction(self, min_time, min_time_ind):
        """Execute the fastest reaction.
        
        Arguments:
            min_time {float} 
            min_time_ind {float} 
        """

        if min_time == np.inf:
            self.clock += 0
        else:
            for j in self.products[min_time_ind]:
                j.increase()
            for i in self.reactants[min_time_ind]:
                i.decrease() 
            self.clock += min_time
        
class Cell(object):

    def __init__(self, voxel, cell_d, k_diff):
        """Initialize the Cell object.
        
        Arguments:
            voxel {Voxel object} 
            cell_d {tuple} -- The dimensions of a cell, which specifies 
                              the number of voxels in a cell.
            k_diff {float} -- The reaction rate of the diffusion of PTEN.
        """

        assert type(cell_d) == tuple
        self.init_voxel = voxel
        self.cell_d = cell_d
        self.k_diff = k_diff
        self.coordinates = list(product(xrange(self.cell_d[0]), xrange(self.cell_d[1])))
        self.voxels = []
        for i in self.coordinates:
            voxel = deepcopy(self.init_voxel)
            voxel.set_loc(i)
            voxel.init_species_num()
            self.voxels.append(voxel)
        self.n_voxels = len(self.coordinates)
        self.clock = 0
        self.init_history()

    def init_history(self):
        self.history = {}
        self.history['RAS'] = []

    def update_history(self):
        self.history['RAS'].append((np.mean([i.RAS.n for i in self.voxels]), np.var([i.RAS.n for i in self.voxels])))
        
    def diffuse(self, voxel):
        """Diffuse 1 PTEN from a voxel to one of its neighbors.
        
        Arguments:
            voxel {Voxel object} 
        """

        diff_reactions = []
        neighbor_L = (voxel.loc[0] - 1, voxel.loc[1])
        neighbor_R = (voxel.loc[0] + 1, voxel.loc[1])
        neighbor_U = (voxel.loc[0], voxel.loc[1] + 1)
        neighbor_D = (voxel.loc[0], voxel.loc[1] - 1)
        for i in range(len(self.coordinates)):
            if self.coordinates[i] == neighbor_L:
                diff_reactions.append(neighbor_L)
            elif self.coordinates[i] == neighbor_R:
                diff_reactions.append(neighbor_R)
            elif self.coordinates[i] == neighbor_U:
                diff_reactions.append(neighbor_U)
            elif self.coordinates[i] == neighbor_D:
                diff_reactions.append(neighbor_D)
        prop = voxel.PTEN.n * self.k_diff
        if prop:
            times = [1./prop*(np.log(1./(rnd.random()))) for i in range(len(diff_reactions))]
            min_time_ind = np.argmin(times)
            voxel.PTEN.decrease()
            self.voxels[self.coordinates.index(diff_reactions[min_time_ind])].PTEN.increase()

    def simulate_step(self):
        """Simulate all the next reactions in all the voxels of the cell.
        """

        times = np.zeros(self.n_voxels)
        times_ind = np.zeros(self.n_voxels)
        for i in range(self.n_voxels):
            t, t_ind = self.voxels[i].next_reaction_method()
            times[i] = t
            times_ind[i] = t_ind
        sorted_times = np.argsort(times)
        for i in sorted_times:
            self.voxels[i].execute_reaction(times[i], times_ind[i])
            self.diffuse(self.voxels[i])
        self.clock += sorted_times[-1]
        self.update_history()

    def simulate_cell(self, T, verbose=False):
        for i in range(T):
            self.simulate_step()
            if verbose:
                if not i%1000:
                    print(i),

        


