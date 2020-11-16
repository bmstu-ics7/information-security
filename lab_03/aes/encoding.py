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

    state = functions.shift_rows(state)
    state = functions.mix_columns(state)
    state = functions.xor_lap_key(state, keys, lap + 1)

    result = [None for _ in range(4 * nb)]
    for i in range(4):
        result += state[i]
    return result


def decode(message: [int], password: str) -> [int]:
    state = [[] for _ in range(nb)]
    for i in range(4):
        for j in range(nb):
            state[i].append(message[i + 4 * j])

    keys = functions.generate_keys(password)

    for lap in range(nr - 1, 0, -1):
        state = functions.shift_rows(state, decoding=True)
        state = functions.sub_bytes(state, decoding=True)
        state = functions.xor_lap_key(state, keys, lap)
        state = functions.mix_columns(state, decoding=True)

    state = functions.shift_rows(state, decoding=True)
    state = functions.sub_bytes(state, decoding=True)
    state = functions.mix_columns(state, decoding=True)

    result = [None for _ in range(4 * nb)]
    for i in range(4):
        result += state[i]
    return result


if __name__ == "__main__":
    print(encode("", ""))
