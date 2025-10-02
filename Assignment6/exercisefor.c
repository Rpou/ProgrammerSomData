void main(int m) {
	
	int arr[4];
	arr[0] = 7;
	arr[1] = 13;
	arr[2] = 9;
	arr[3] = 8;
	int sum;
	sum = 0;	
	arrsum(4, arr, &sum);
	print sum;
	println;

	int arr2[20];
	squares(m, arr2);
	int sum2;
	sum2 = 0;
	arrsum(m, arr2, &sum2);
        print sum2;
	println;

	int arr3[7];
	arr3[0] = 1;	
	arr3[1] = 2;	
	arr3[2] = 1;	
	arr3[3] = 1;	
	arr3[4] = 1;	
	arr3[5] = 2;	
	arr3[6] = 0;
	int freq[4];
	freq[0] = 0;
	freq[1] = 0;
	freq[2] = 0;
	freq[3] = 0;
	histogram(7, arr3, 3, freq);
	int i;
	for(i=0; i < 4; ++i){
		print freq[i];
		println;	
	}
	println;
}

void arrsum(int n, int arr[], int *sump){
	int i;
	for(i = 0; i < n; i = i + 1){
		*sump = *sump + arr[i];
	}

}

void squares(int n, int arr[]){
	int i;
	for(i = 0; i < n; i = i + 1){
		arr[i] = i * i;
	}
}

void histogram(int n, int ns[], int max, int freq[]){
	int i;
	for(i = 0; i < n; i = i + 1){	
		freq[ns[i]] = freq[ns[i]] + 1;
	}
}
