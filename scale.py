'''
scale.py

Geometrically scale a Vintage Story model.

Assumes no texturing has been done yet, should be used in the model stage.
'''
import json
import sys

def scale_element(element, factor):
    '''
    Scales `element` and children by `factor` recursively.
    '''
    # TODO: Scale numbers in "from".
    # TODO: Scale numbers in "to".
    # TODO: Call on each of the "children" keys.

def scale(model, factor: int) -> dict:
    '''
    Scales all the element sizes and positions in `model` by `factor`.
    Takes an object already parsed into a Python dictionary, and
    returns a Python dictionary to be converted back to JSON.
    '''
    # TODO: Check for no elements key.
    # TODO: Check for no element in "elements".
    # TODO: Call recursive scaler on elements.
    
if __name__ == "__main__":
    # TODO: Check contents of sys.argv, print usage if invalid.
    # TODO: Open input file,
    # TODO: parse JSON,
    # TODO: scale JSON,
    # TODO: dump to output file. 
