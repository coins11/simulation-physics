import random
for alpha in (0.3,0.5,0.7):
	t1=[]
	[t1.append(0) for i in range(0, 20-int(alpha*20))] and [t1.append(1) for i in range(0, int(alpha*20))] and random.seed(149) or random.shuffle(t1)
	print "\ndensity:	%f\nt= 0  "%alpha,"".join(map((lambda x:str(x)+" "), t1)) or ([t1.append(0) for i in range(0, 20-int(alpha*20))] and [t1.append(1) for i in range(0, int(alpha*20))] and random.seed(149) or random.shuffle(t1))
	for i in range(1,21):
		t2=[]
		[t2.append((0,0,0,1,1,1,0,1)[int("".join(map(str, map((lambda x:t1[x]), j-1<0 and (19,0,1) or j+1>=20 and (18,19,0) or (j-1,j,j+1)))),2)]) for j in range(0,20)]
		t1=t2[:]
		print "t=%2d  "%i,"".join(map((lambda x:str(x)+" "), t2))
