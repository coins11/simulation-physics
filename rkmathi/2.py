import random
rule    = {'111':'1', '110':'0', '101':'1', '100':'1', '011':'1', '010':'0', '001':'0', '000':'0'}
density = [0.3, 0.5, 0.7]
if __name__ == '__main__':
    random.seed(149)
    for i in density:
        n, m = [], []
        print('density = %f'% i)
        ratio = int(20 * i)
        [n.append(1) for j in range(ratio)]
        [n.append(0) for j in range(20-ratio)]
        random.shuffle(n)
        m.append(n)
        for j in range(21):
            p = []
            for k in range(21):
                if k == 0:
                    p.append((rule["".join((str(m[j][19]) , str(m[j][k]), str(m[j][k+1])))]))
                if 1 <= k <= 18:
                    p.append( rule["".join((str(m[j][k-1]), str(m[j][k]), str(m[j][k+1])))])
                if k == 19:
                    p.append((rule["".join((str(m[j][k-1]), str(m[j][k]), str(m[j][0]  )))]))
            m.append(list(map(int, p)))
            print("t = %2d" % (j), m[j])
