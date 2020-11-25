#!/usr/bin/env python3

import sys
import argparse
import message
import file
from rsa import RSA
from exceptions import ParameterException


def create_parser():
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "-f", "--file",
        required=False, type=str,
        help="file, that encode"
    )
    parser.add_argument(
        "-o", "--out",
        required=False, type=str, default="out",
        help="output file"
    )
    parser.add_argument(
        "-m", "--message",
        required=False, type=str,
        help="message for encode"
    )
    parser.add_argument(
        "-p", "--p",
        required=False, type=int,
        help="first simple number"
    )
    parser.add_argument(
        "-q", "--q",
        required=False, type=int,
        help="second simple number"
    )
    parser.add_argument(
        "-s", "--seed",
        required=False, type=int,
        help="seed for random"
    )
    parser.add_argument(
        "-d", "--decode", nargs='?',
        const=True, default=False, type=bool,
        help="decode mode (default disabled)"
    )
    return parser


def main():
    parser = create_parser()
    args = parser.parse_args(sys.argv[1:])
    rsa = RSA(p=args.p, q=args.q, seed=args.seed)
    print("P:", rsa.p)
    print("Q:", rsa.q)
    print("N:", rsa.n)
    print("E:", rsa.e)
    print("D:", rsa.d)
    if args.message is not None:
        message.encode_message(args.message, rsa, args.decode)
    elif args.file is not None:
        file.encode_file(args.file, args.out, rsa, args.decode)
    else:
        raise ParameterException(
            "Need message or file parameter, add -h to show help"
        )


if __name__ == "__main__":
    main()
