def shift(array: [], count: int, right: bool) -> []:
    length = len(array)

    for _ in range(count):
        if right:
            array = [array[length - 1]] + array[:length - 1]
        else:
            array = array[1:] + [array[0]]

    return array
