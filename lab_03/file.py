from aes.encoding import encode, decode


def encode_file(filename: str, outfile: str, password: str, decoding: bool):
    input_file = open(filename, 'rb')
    output_file = open(outfile, 'wb')
    byte = input_file.read(16)
    while byte:
        bytes_to_write = b""
        current = [b for b in byte]
        if len(current) < 16:
            zeros = 16 - len(current)
            current += [1] + [0] * (zeros - 1)
        current = encode(current, password) \
            if not decoding else decode(current, password)
        byte = input_file.read(16)
        if decoding and not byte:
            for i in range(len(current) - 1, -1, -1):
                if current[i] == 1:
                    index = i
            current = current[:index]
            if len(current) == 0:
                break

        bytes_to_write += bytes(current)
        output_file.write(bytes_to_write)
    input_file.close()
    output_file.close()
