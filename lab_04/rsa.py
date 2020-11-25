#!/usr/bin/env python3

import number
import random


class RSA:
    def __init__(self, p=None, q=None, seed=None):
        if seed:
            random.seed(seed)
        self._p = p
        if not p or not number.is_simple(p):
            self._p = number.get_random_simple(100, 255)
        self._q = q
        if not q or not number.is_simple(q):
            self._q = number.get_random_simple(100, 255)
        while self._p == self._q:
            self._q = number.get_random_simple(100, 255)
        self._n = self._p * self._q
        self._phi = (self._p - 1) * (self._q - 1)
        self._e = self._get_e()
        self._d = self._get_d()

    def _get_e(self):
        while True:
            n = random.randint(2, 255)
            if number.gcd(n, self._phi) == 1:
                return n

    def _get_d(self):
        n = self._phi + 1
        while True:
            if n % self._e == 0:
                return n // self._e
            n += self._phi

    def crypt(self, symbol, key):
        return pow(symbol, key, self._n)

    def encrypt(self, string):
        return "".join([chr(self.crypt(ord(sym), self._e)) for sym in string])

    def decrypt(self, string):
        return "".join([chr(self.crypt(ord(sym), self._d)) for sym in string])

    @property
    def p(self):
        return self._p

    @property
    def q(self):
        return self._q

    @property
    def n(self):
        return self._n

    @property
    def e(self):
        return self._e

    @property
    def d(self):
        return self._d


if __name__ == "__main__":
    rsa = RSA()
    print("P:", rsa.p, sep="\t")
    print("Q:", rsa.q, sep="\t")
    print("N:", rsa.n, sep="\t")
    print("E:", rsa.e, sep="\t")
    print("D:", rsa.d, sep="\t")
    message = "Hello, rsa!"
    print("message:", message)
    message = rsa.encrypt(message)
    print("encrypt:", message)
    message = rsa.decrypt(message)
    print("decrypt:", message)
