# Compute gaussian weights, for use in the blur shader.
import math

def generate_gaussian_kernel(radius, sigma):
    size = 2 * radius + 1
    kernel = []
    sum_val = 0.0
    
    for i in range(size):
        x = i - radius
        value = math.exp(-(x * x) / (2 * sigma * sigma)) / (math.sqrt(2 * math.pi) * sigma)
        kernel.append(value)
        sum_val += value
    
    # Normalize the kernel
    kernel = [x / sum_val for x in kernel]
    
    return kernel

radius = 20
sigma = 10.0
kernel = generate_gaussian_kernel(radius, sigma)

print(F"Gaussian Kernel Length: {len(kernel)}")
print("Gaussian Kernel Weights:")
print(kernel)
