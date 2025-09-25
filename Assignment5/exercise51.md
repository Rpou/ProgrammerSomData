## f# implementation af mergesort på 2 sorterede int lister

    let rec merge (list1: int list) (list2: int list) : int list =
        match list1, list2 with
        | [], _ -> list2
        | _, [] -> list1
        | x :: xs, y :: ys -> if x > y then y :: merge list1 ys
                              else x :: merge xs list2

## Java implementation af mergesort på 2 sorterede arr

    public static int[] merge(int[] list1, int[] list2){
        int x = 0;
        int y = 0;
        int[] newList = new int[list1.length + list2.length];
        int i = 0;

        while(x < list1.length && y < list2.length){
            if (list1[x] > list2[y]){
                newList[i] = list2[y];
                y++;
            }
            else {
                newList[i] = list1[x];
                x++;
            }
            i++;
        }

        while(x < list1.length){
            newList[i] = list1[x];
            x++;
            i++;
        }

        while(y < list2.length){
            newList[i] = list1[y];
            y++;
            i++;
        }

        return newList;
    }
