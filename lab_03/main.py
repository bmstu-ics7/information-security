#!/usr/bin/env python3

import sys
import argparse
import message
import file
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
        "-p", "--password",
        required=True, type=str,
        help="secret password for encode and decode"
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
    if args.message is not None:
        message.encode_message(args.message, args.password, args.decode)
    elif args.file is not None:
        file.encode_file(args.file, args.out, args.password, args.decode)
    else:
        raise ParameterException(
            "Need message or file parameter, add -h to show help"
        )


if __name__ == "__main__":
    main()
