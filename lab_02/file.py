def encode_file(enigma, filename, outfile):
    input_file = open(filename, 'rb')
    output_file = open(outfile, 'wb')
    byte = input_file.read(10)
    while byte:
        output_file.write(enigma.encode(byte))
        byte = input_file.read(10)
    input_file.close()
    output_file.close()
