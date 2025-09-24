## f# implementation af mergesort på 2 int lister

    let rec merge (list1: int list) (list2: int list) : int list =
        match list1, list2 with
        | [], _ -> list2
        | _, [] -> list1
        | x :: xs, y :: ys -> if x > y then y :: merge list1 ys
                              else x :: merge xs list2
