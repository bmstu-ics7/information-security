from aes.encoding import encode, decode


def encode_message(message: str, password: str, decoding: bool):
    print("Message:", message)
    bytes_message = [ord(c) for c in message]
    count = len(bytes_message) // 16
    result = []
    for i in range(count + 1):
        if (i + 1) * 16 > len(bytes_message):
            current = bytes_message[count * 16:]
            zeros = 16 - len(current)
            current += [1] + [0] * (zeros - 1)
        else:
            current = bytes_message[(i * 16):((i + 1) * 16)]
        result += encode(current, password) \
            if not decoding else decode(current, password)
    message = [chr(c) for c in result]
    print(
        "Encoded" if not decoding else "Decoded", "message:", message
    )
    print(
        "Encoded" if not decoding else "Decoded", "message:", "".join(message)
    )
