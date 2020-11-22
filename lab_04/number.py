import random
from math import sqrt


def get_random_simple(a, b):
    simple = [True for _ in range(b + 1)]
    simple[0] = False
    simple[1] = False
    for p in range(2, int(sqrt(b))):
        if simple[p]:
            for i in range(p, len(simple), p):
                simple[i] = False
    numbers = []
    for n in range(a, b + 1):
        if simple[n]:
            numbers.append(n)
    return random.choice(numbers)


def is_simple(a):
    for n in range(2, int(sqrt(a))):
        if a % n == 0:
            return False
    return True


def gcd(a, b):
    while b:
        a, b = b, a % b
    return a
