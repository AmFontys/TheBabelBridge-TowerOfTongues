# How the benchmark pipeline works

## Introduction

This document is about the CI/CD pipeline in GitHub for the benchmarking project.

## What is being benchmarked

The benchmarking in this project focuses on the performance of different methods for generating crossword puzzles using the `CrosswordCoreBenchmark` class. The benchmarks are designed to measure how efficiently crossword puzzles can be constructed under various scenarios and input sizes.

Specifically, the following methods are benchmarked:

- **TestMethodGenerateCrossword:** This method benchmarks the core crossword generation logic by creating a list of `CrosswordWord` objects in memory and passing them to the crossword generator. The number of words is varied using the `[Params(10, 100, 1000, 10000)]` attribute to observe how performance scales with input size.

- **TestCsvGenerateCrosswordWithTake:** This method benchmarks the performance of generating crosswords using a subset of words read from a CSV file. this allows it for performance comparison between the regular `TestMethodGenerateCrossword` method. As there can be words that are generally more difficult to place then others.

- **TestCsvGenerateCrosswordWithRandomSampling:** This method benchmarks crossword generation using a random sample of words from the CSV file. It randomly selects the specified number of words and then generates the crossword, providing insight into performance when the input is not sequential.

Each benchmark method is executed with different input sizes (`AmountOfWords`) to analyze scalability and which method is the fastest.  

Data that is important for me to see are things such as the minimum, maximum, mean, and median execution times, as well as memory usage.  
This is handy to know if there is a possible "fluke" in execution time such as one time it went extremely fast.  
The benchmarks results are exported for further analysis and reporting via a github pipeline.

To investigate whether the script or writing system of the words affects crossword generation performance, I created a separate benchmark class called `CrosswordLangComparison`. This class benchmarks the crossword generation process using word lists written in different scripts: Latin, Braille, and Hiragana. Each method reads a CSV file containing words in a specific script, randomly samples a specified number of words (using the same `[Params(10, 100, 1000, 10000)]` for input size), and then generates a crossword with those words.

The following methods are benchmarked:

- **TestCsvGenerateCrosswordWithRandomSamplingLatinScript:** Benchmarks crossword generation using randomly sampled words from a CSV file containing Latin script words.
- **TestCsvGenerateCrosswordWithRandomSamplingBrailleScript:** Benchmarks crossword generation using randomly sampled words from a CSV file containing Braille script words.
- **TestCsvGenerateCrosswordWithRandomSamplingHiraganaScript:** Benchmarks crossword generation using randomly sampled words from a CSV file containing Hiragana script words.

By comparing the results of these benchmarks, it is possible to determine if the underlying writing system has any significant impact on the performance or memory usage of the crossword generation process. This helps ensure that the generator performs efficiently and consistently across different languages and scripts which is one of the main points of my project.
  
### Key Findings from Script Comparison

The benchmark results show several key findings:

- **For smaller input sizes (10 or 100 words):**  
Braille script is generally faster than both Latin and Hiragana scripts for crossword generation with smaller input sizes. For 10 words, Braille's mean execution time is 7.89 ms, compared to 6.15 ms for Latin and 6.85 ms for Hiragana.  
The difference between Braille and Latin is about 1.74 ms, and between Braille and Hiragana about 1.04 ms. For 100 words, Braille's mean is 11.31 ms, Latin is 8.59 ms, and Hiragana is 6.96 ms.  
Here, Braille is about 2.72 ms slower than Latin, but Latin is 1.63 ms slower than Hiragana. The differences are noticeable but not extreme for small word lists.

- **For larger input sizes (1,000 or 10,000 words):**  
Latin script becomes significantly slower compared to Braille and Hiragana. This is likely due to the increased number of possible intersections and the greater diversity of characters in the Latin script, which makes finding valid placements more computationally intensive as the word list grows. For example, with 10,000 words, Latin's mean execution time is much higher than both Braille and Hiragana, highlighting the scalability challenges with Latin script in larger datasets.

## Purpose of the Benchmark Pipeline

The purpose of this pipeline is to get an artifact of the benchmark that can be used to track the methods that are being benchmarked on how long they take and which is better in terms of performance or other.

## Overview of the CI/CD Pipeline

The CI/CD pipeline for the benchmarking project is implemented using GitHub Actions and is defined in the `benchmark.yml` workflow file. This pipeline is triggered automatically on pushes to the `main` and `development` branches.

The pipeline consists of the following steps:

1. **Code Checkout:** Retrieves the latest version of the code from the repository.
2. **Setup Environment:** Installs the required .NET SDK and restores project dependencies.
3. **Build Stage:** Compiles the solution in release mode to ensure optimized performance for benchmarking.
4. **Test Stage:** Runs automated tests to verify the correctness and stability of the code before benchmarking.
5. **Benchmark Stage:** Executes performance benchmarks using BenchmarkDotNet, capturing metrics such as execution time and memory usage.
6. **Save Results:** Uploads the benchmark results as artifacts, making them available for review and comparison.

This automated workflow ensures that every code change is consistently built, benchmarked, and the results are archived for future analysis.

## Pipeline Stages

### 1. Code Checkout

````YML
 - name: Checkout code
      uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '8.0.x'

    - name: Restore dependencies
      run: dotnet restore
````

### 2. Build Stage

````YML
    - name: Build solution
      run: dotnet build --configuration Release
````

### 3. Test Stage

````YML
    - name: test solution
      run: dotnet test --configuration Release
````

### 4. Benchmark Stage

````YML
  - name: Run benchmarks
      run: dotnet run --project BBTT/BBTT.BenchMarkingConsole/BBTT.BenchMarkingConsole.csproj --configuration Release > BenchmarkResult.txt
````

### 5. Save the results

````YML
   - name: Upload benchmark results
      uses: actions/upload-artifact@v4
      with:
        name: benchmark-results-${{ github.run_number }}
        path: BenchmarkResult.txt
````

## Tools and Technologies Used

I used github actions to run this pipeline and the benchmarking was done by using the benchmarkingDotNet Nuget package. Furthermore the benchmarking exists out of 2 projects a console project which activates the benchmarking and connects to all the parts that are needed to be benchmarked. The second project is a class library to isolate the function and that unneeded things are not being connected to it in anyway.

## Example Pipeline Workflow

### Example Benchmark Results

Below is a sample outcome from running the benchmarking pipeline. The table summarizes performance metrics for each method and input size:

| **Method**                                | **Words** | **Mean**      | **StdDev**    | **Median**    | **Min**        | **Max**        | **Ratio** | **RatioSD** | **Gen0**   | **Gen1**   | **Gen2**   | **Allocated** | **Alloc Ratio** |
|-------------------------------------------|:---------:|--------------:|--------------:|--------------:|---------------:|---------------:|----------:|------------:|-----------:|-----------:|-----------:|--------------:|---------------:|
| TestMethodGenerateCrossword                | 10        |   12.20 μs    |   1.72 μs     |   11.59 μs    |    9.68 μs     |   15.95 μs     |    1.02   |      0.20   |    1.22    |      -     |      -     |    10.05 KB   |         1.00   |
| TestCsvGenerateCrosswordWithTake           | 10        |  974.48 μs    |  73.82 μs     |  974.11 μs    |  755.50 μs     | 1,139.32 μs    |   81.37   |     12.35   |    7.81    |    5.86    |      -     |    72.67 KB   |         7.23   |
| TestCsvGenerateCrosswordWithRandomSampling | 10        | 12,542.84 μs  | 1,610.82 μs   | 12,739.89 μs  | 9,648.31 μs    | 16,547.94 μs   | 1,047.29  |    192.73   |  390.63    |  375.00    |  171.88    | 3,234.36 KB   |       321.68   |
|                                           |           |               |               |               |                |                |           |             |            |            |            |               |                |
| TestMethodGenerateCrossword                | 100       |  132.22 μs    |  10.42 μs     |  129.10 μs    |  113.79 μs     |   162.02 μs    |    1.01   |      0.11   |   12.94    |    0.73    |      -     |   107.48 KB   |         1.00   |
| TestCsvGenerateCrosswordWithTake           | 100       | 1,328.39 μs   | 112.19 μs     | 1,316.87 μs   | 1,064.72 μs    | 1,617.19 μs    |   10.11   |      1.14   |   35.16    |   11.72    |      -     |   299.54 KB   |         2.79   |
| TestCsvGenerateCrosswordWithRandomSampling | 100       | 11,847.48 μs  | 723.06 μs     | 11,729.93 μs  | 10,623.29 μs   | 13,749.14 μs   |   90.13   |      8.71   |  421.88    |  390.63    |  171.88    | 3,441.20 KB   |        32.02   |
|                                           |           |               |               |               |                |                |           |             |            |            |            |               |                |
| TestMethodGenerateCrossword                | 1,000     | 2,313.39 μs   | 138.51 μs     | 2,286.55 μs   | 2,051.31 μs    | 2,616.74 μs    |    1.00   |      0.08   |  289.06    |  140.63    |      -     | 2,364.05 KB   |         1.00   |
| TestCsvGenerateCrosswordWithTake           | 1,000     | 13,677.02 μs  | 1,094.40 μs   | 13,636.93 μs  | 11,753.23 μs   | 16,143.82 μs   |    5.93   |      0.58   | 1,062.50   |      -     |      -     | 8,701.59 KB   |         3.68   |
| TestCsvGenerateCrosswordWithRandomSampling | 1,000     | 23,334.14 μs  | 1,220.77 μs   | 23,105.10 μs  | 21,168.68 μs   | 26,762.34 μs   |   10.12   |      0.79   | 1,375.00   |  625.00    |  312.50    | 11,482.29 KB  |         4.86   |
|                                           |           |               |               |               |                |                |           |             |            |            |            |               |                |
| TestMethodGenerateCrossword                | 10,000    | 84,025.20 μs  | 4,496.89 μs   | 83,510.89 μs  | 75,291.17 μs   | 95,544.57 μs   |    1.00   |      0.07   | 10,857.14  | 1,000.00   |  857.14    | 89,113.32 KB  |         1.00   |
| TestCsvGenerateCrosswordWithTake           | 10,000    | 594,529.85 μs | 39,203.56 μs  | 591,200.30 μs | 514,175.40 μs  | 703,566.50 μs  |    7.10   |      0.60   | 48,000.00  | 1,000.00   |      -     | 395,695.02 KB |         4.44   |
| TestCsvGenerateCrosswordWithRandomSampling | 10,000    | 607,793.02 μs | 31,484.41 μs  | 608,037.50 μs | 541,894.70 μs  | 678,937.50 μs  |    7.25   |      0.53   | 48,000.00  | 1,000.00   |      -     | 394,541.73 KB |         4.43   |

#### Column Descriptions

- **Words**: Number of words used in the benchmark.
- **Mean**: Average execution time.
- **StdDev**: Standard deviation of measurements.
- **Median**: 50th percentile execution time.
- **Min/Max**: Minimum and maximum observed times.
- **Ratio/RatioSD**: Relative performance compared to baseline.
- **Gen0/Gen1/Gen2**: Garbage collections per 1,000 operations (by generation).
- **Allocated**: Managed memory allocated per operation.
- **Alloc Ratio**: Memory allocation ratio compared to baseline.
- **μs**: Microseconds (1 μs = 0.000001 seconds).

This table helps visualize the performance and memory usage of each method as the input size increases.

## Conclusion

In conclusion, the benchmarking pipeline provides an automated and consistent way to evaluate the performance of different crossword generation methods as the codebase evolves.  
By integrating BenchmarkDotNet with GitHub Actions, every change to the project is automatically tested and benchmarked, ensuring that performance regressions are detected early.  
The collected benchmark results offer valuable insights into execution time and memory usage across various input sizes, helping guide future optimizations and design decisions. This approach supports continuous improvement and maintains high performance standards for the project.
