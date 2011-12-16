from random  import *
from decimal import *

seeds = [149,193,251,383,457,503,691,761,829,991]

def monteCarlo(n):
    count = 0.0
    for s in seed:
        seed(s)
        for i in range(0, n)
            x = random()
            y = random()
            if x**2 + y**2 <= 1.0:
                count += 1


    for i in range(n):
        x, y = uniform(0, 1), uniform(0, 1)
        if x**2.0 + y**2.0 <= 1.0:
            print(x, y)
            count += 1.0
    pi = 4*count/n
    print(pi)

def main():
    monteCarlo(10)

if __name__ == '__main__':
    main()
