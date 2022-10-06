# Counts from 100 to 1
# If number is divisible by 5 and 3 prints "Testing"
# If number is divisible by 5 prints "Agile"
# If number is divisible by 3 and 3 prints "Software"
# If none of conditions is true prints number

for x in range(100, 0, -1):
    if x % 5 == 0 and x % 3 == 0 :
        print("Testing")
    elif x % 5 == 0 :
        print("Agile")
    elif x % 3 == 0 :
        print("Software")
    else :
        print(x)