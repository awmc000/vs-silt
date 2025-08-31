'''
scale.py

Geometrically scale a Vintage Story model.

Assumes no texturing has been done yet, should be used in the model stage.
usage: python vsmc_scale.py {input filename} {factor} {output filename}
example: python vsmc_scale.py bunny.json 5 bigbunny.json
'''
import json
import sys

def scale_element(element, factor):
    '''
    Scales `element` and children by `factor` recursively.
    Mutates `element`.
    '''
    # Handle error: called on null
    if element is None:
        raise ValueError('Called on null!')

    # Handle error: does not have expected keys
    expectedKeys = [
        'name',
        'from',
        'to',
    ]
    for ek in expectedKeys:
        if ek not in element:
            print(f'{ek} not in element: {element.keys()}')
            raise ValueError('bad schema')

    # Scale numbers in "from".
    for i in range(len(element["from"])):
        print(f'Scaling {element["name"]}.from from {element["from"][i]} to {element["from"][i] * factor}.')
        element["from"][i] *= factor

    # Scale numbers in "to".
    for i in range(len(element["to"])):
        print(f'Scaling {element["name"]}.to from {element["to"][i]} to {element["to"][i] * factor}.')
        element["to"][i] *= factor

    # Scale numbers in "rotationOrigin"
    if 'rotationOrigin' in element:
        for i in range(len(element["rotationOrigin"])):
            print(f'Scaling {element["name"]}.to from {element["rotationOrigin"][i]} to {element["rotationOrigin"][i] * factor}.')
            element["rotationOrigin"][i] *= factor

    # Recursive case: Call on each of the "children" keys.
    if 'children' in element.keys():
        for child in element["children"]:
            scale_element(child, factor)

def scale(model, factor: int) -> dict:
    '''
    Scales all the element sizes and positions in `model` by `factor`.
    Takes an object already parsed into a Python dictionary, and
    returns a Python dictionary to be converted back to JSON.
    Mutates `model`.
    '''
    # Handle error: not expected keys
    expectedKeys = [
        'editor',
        'textureWidth',
        'textureHeight',
        'textures',
        'elements'
    ]
    # Call recursive scaler on elements.
    for elem in model['elements']:
        scale_element(elem, factor)
    return model

if __name__ == "__main__":
    # Check contents of sys.argv, print usage if invalid.
    if len(sys.argv) != 4:
        print(
            'usage: python vsmc_scale.py {input filename} {factor} {output filename}\n'
            'example: python vsmc_scale.py bunny.json 5 bigbunny.json'
        )
        sys.exit(0)

    in_file = sys.argv[1]
    scale_factor = int(sys.argv[2])
    out_file = sys.argv[3]

    model = None

    # Open input file, parse JSON to object
    with open(in_file) as file:
        model = json.loads(file.read())

    # scale JSON,
    scale(model, scale_factor)

    # dump to output file.
    with open(out_file, 'w') as file:
        file.write(json.dumps(model))
