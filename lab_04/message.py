def encode_message(message: str, rsa, decoding: bool):
    print("Message:", message)
    message = rsa.encrypt(message) if not decoding else rsa.decrypt(message)
    print(
        "Encoded" if not decoding else "Decoded", "message:", message
    )
