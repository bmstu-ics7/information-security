from . import variables as var
from . import array
from . import GF


def generate_keys(password):
    key = [ord(symbol) for symbol in password]

    if len(key) < 4 * var.nk:
        for i in range(4 * var.nk - len(key)):
            key.append(0x01)

    result = [[] for _ in range(4)]
    for i in range(4):
        for j in range(var.nk):
            result[i].append(key[i + 4 * j])

    for j in range(var.nk, var.nb * (var.nr + 1)):
        if j % var.nk == 0:
            tmp = [result[i][j - 1] for i in range(1, 4)]
            tmp.append(result[0][j - 1])

            for i in range(len(tmp)):
                tmp[i] = var.sbox[tmp[i]]

            for i in range(4):
                s = result[i][j - 4] ^ tmp[i] ^ var.rcon[i][j // var.nk - 1]
                result[i].append(s)
        else:
            for i in range(4):
                s = result[i][j - 4] ^ result[i][j - 1]
                result[i].append(s)

    return result


def sub_bytes(state, decoding=False):
    box = []
    if not decoding:
        box = var.sbox
    else:
        box = var.inv_sbox

    for i in range(len(state)):
        for j in range(len(state[i])):
            state[i][j] = box[state[i][j]]

    return state


def shift_rows(state, decoding=False):
    for i in range(1, var.nb):
        state[i] = array.shift(state[i], i, decoding)
    return state


def mix_columns(state, decoding=False):
    for i in range(var.nb):
        if not decoding:
            s0 = GF.mul_by_02(state[0][i]) ^ GF.mul_by_03(state[1][i]) ^ \
                state[2][i] ^ state[3][i]
            s1 = state[0][i] ^ GF.mul_by_02(state[1][i]) ^ \
                GF.mul_by_03(state[2][i]) ^ state[3][i]
            s2 = state[0][i] ^ state[1][i] ^ GF.mul_by_02(state[2][i]) ^ \
                GF.mul_by_03(state[3][i])
            s3 = GF.mul_by_03(state[0][i]) ^ state[1][i] ^ state[2][i] ^ \
                GF.mul_by_02(state[3][i])
        else:
            s0 = GF.mul_by_0e(state[0][i]) ^ GF.mul_by_0b(state[1][i]) ^ \
                GF.mul_by_0d(state[2][i]) ^ GF.mul_by_09(state[3][i])
            s1 = GF.mul_by_09(state[0][i]) ^ GF.mul_by_0e(state[1][i]) ^ \
                GF.mul_by_0b(state[2][i]) ^ GF.mul_by_0d(state[3][i])
            s2 = GF.mul_by_0d(state[0][i]) ^ GF.mul_by_09(state[1][i]) ^ \
                GF.mul_by_0e(state[2][i]) ^ GF.mul_by_0b(state[3][i])
            s3 = GF.mul_by_0b(state[0][i]) ^ GF.mul_by_0d(state[1][i]) ^ \
                GF.mul_by_09(state[2][i]) ^ GF.mul_by_0e(state[3][i])

        state[0][i] = s0
        state[1][i] = s1
        state[2][i] = s2
        state[3][i] = s3

    return state


def xor_lap_key(state, keys, lap):
    for j in range(var.nk):
        for i in range(4):
            state[i][j] ^= keys[i][var.nb * lap + j]
    return state
