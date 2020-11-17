#!/usr/bin/env python3

from variables import nb, nr
import functions


def encode(message: [int], password: str) -> [int]:
    state = [[] for _ in range(4)]
    for i in range(4):
        for j in range(nb):
            state[i].append(message[i + 4 * j])

    keys = functions.generate_keys(password)
    state = functions.xor_lap_key(state, keys, 0)

    for lap in range(1, nr):
        state = functions.sub_bytes(state)
        state = functions.shift_rows(state)
        state = functions.mix_columns(state)
        state = functions.xor_lap_key(state, keys, lap)

    state = functions.sub_bytes(state)
    state = functions.shift_rows(state)
    state = functions.xor_lap_key(state, keys, lap + 1)

    result = [None for i in range(4 * nb)]
    for i in range(4):
        for j in range(nb):
            result[i + 4 * j] = state[i][j]
    return result


def decode(message: [int], password: str) -> [int]:
    state = [[] for _ in range(nb)]
    for i in range(4):
        for j in range(nb):
            state[i].append(message[i + 4 * j])

    keys = functions.generate_keys(password)
    state = functions.xor_lap_key(state, keys, nr)

    for lap in range(nr - 1, 0, -1):
        state = functions.shift_rows(state, decoding=True)
        state = functions.sub_bytes(state, decoding=True)
        state = functions.xor_lap_key(state, keys, lap)
        state = functions.mix_columns(state, decoding=True)

    state = functions.shift_rows(state, decoding=True)
    state = functions.sub_bytes(state, decoding=True)
    state = functions.xor_lap_key(state, keys, 0)

    result = [None for i in range(4 * nb)]
    for i in range(4):
        for j in range(nb):
            result[i + 4 * j] = state[i][j]
    return result


if __name__ == "__main__":
    import random
    message = [random.randint(0, 255) for _ in range(4 * nb)]
    print("message:", message)
    message = encode(message, "hi good password")
    print("encode: ", message)
    message = decode(message, "hi good password")
    print("message:", message)
