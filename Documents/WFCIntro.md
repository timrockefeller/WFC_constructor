## Algorithm

1. Read the input bitmap and count NxN patterns.
    1. (optional) Augment pattern data with rotations and reflections.
2. Create an array with the dimensions of the output (called "wave" in the source). Each element of this array represents a state of an NxN region in the output. A state of an NxN region is a superposition of NxN patterns of the input with boolean coefficients (so a state of a pixel in the output is a superposition of input colors with real coefficients). False coefficient means that the corresponding pattern is forbidden, true coefficient means that the corresponding pattern is not yet forbidden.
3. Initialize the wave in the completely unobserved state, i.e. with all the boolean coefficients being true.
4. Repeat the following steps:
    1. Observation:
       1. Find a wave element with the minimal nonzero entropy. If there is no such elements (if all elements have zero or undefined entropy) then break the cycle (4) and go to step (5).
       2. Collapse this element into a definite state according to its coefficients and the distribution of NxN patterns in the input.
    2. Propagation: propagate information gained on the previous observation step.
5. By now all the wave elements are either in a completely observed state (all the coefficients except one being zero) or in the contradictory state (all the coefficients being zero). In the first case return the output. In the second case finish the work without returning anything.

## 算法

1. 读取输入位图并计算 NxN 模式。
   1. (可选)使用旋转和反射增强图形数据。
2. 创建一个具有输出维度的数组(在源代码中称为“ wave”)。此数组的每个元素表示输出中 NxN 区域的状态。NxN 区域的状态是输入 NxN 模式与布尔系数的叠加(因此输出像素的状态是输入颜色与实际系数的叠加)。虚假系数是指相应的模式被禁止，真实系数是指相应的模式尚未被禁止。
3. 初始化波在完全不观测的状态，即所有的布尔系数为真。
4. 重复以下步骤:
   1. 观测
      1. 求非零熵的最小波元。如果没有这样的元素(如果所有元素都有零或未定义的熵) ，那么结束循环(4) ，进入步骤(5)。
      2. 根据元素的系数和输入中 NxN 模式的分布将元素折叠成一个确定的状态。
   2. 传播：传播在前一个观察步骤中获得的信息。
5. 到目前为止，所有的波元要么处于完全可观测的状态(仅一个系数为零) ，要么处于矛盾状态(所有系数为零)。在第一种情况下，返回输出。在第二种情况下，完成工作而不返还任何东西。