def encode_file(filename: str, outfile: str, rsa, decoding: bool):
    if not decoding:
        input_file = open(filename, 'rb')
        output_file = open(outfile, 'w')
        byte = input_file.read(100)
        while byte:
            bytes_to_write = ""
            for symbol in byte:
                char = chr(symbol)
                bytes_to_write += rsa.encrypt(char)
            output_file.write(bytes_to_write)
            byte = input_file.read(100)
    else:
        input_file = open(filename, 'r')
        output_file = open(outfile, 'wb')
        byte = input_file.read(100)
        while byte:
            bytes_to_write = b""
            for char in byte:
                char = rsa.decrypt(char)
                bytes_to_write += bytes([ord(char)])
            output_file.write(bytes_to_write)
            byte = input_file.read(100)
    input_file.close()
    output_file.close()
