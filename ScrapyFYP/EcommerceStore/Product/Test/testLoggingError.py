class cls():
    a = 0
    b = 1

    def __init__(self) -> None:
        self.b = 2

    def errorFunc(self):
        try:
            c = self.b/self.a
        except Exception as e:
            raise Exception(f'Error occured in testLoggingError {e}')