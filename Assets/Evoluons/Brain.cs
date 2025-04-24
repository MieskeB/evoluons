using System;
using UnityEngine;
using Random = System.Random;

public class Brain
{
    private int inputSize = 7; // health, energy level, happiness, s1, s2, s3, random noise

    private int hiddenSize = 128; // Number of hidden neurons
    private int outputSize = 7; // Move, eat, rest, reproduce, velocity, rotation

    private float[,] inputToHiddenWeights;
    private float[,] hiddenToHiddenWeights;
    private float[,] hiddenToOutputWeights;

    public Brain()
    {
        inputToHiddenWeights = new float[inputSize, hiddenSize];
        hiddenToHiddenWeights = new float[hiddenSize, hiddenSize];
        hiddenToOutputWeights = new float[hiddenSize, outputSize];

        RandomizeWeights();
    }

    private void RandomizeWeights()
    {
        Random rand = new Random();
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                inputToHiddenWeights[i, j] = (float)(rand.NextDouble() * 2 - 1);
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                hiddenToHiddenWeights[i, j] = (float)(rand.NextDouble() * 2 - 1);
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                hiddenToOutputWeights[i, j] = (float)(rand.NextDouble() * 2 - 1);
            }
        }
    }

    public float[] Forward(float[] inputs)
    {
        float[] hidden = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            hidden[i] = 0f;
            for (int j = 0; j < inputSize; j++)
            {
                hidden[i] += inputs[j] * inputToHiddenWeights[j, i];
            }

            hidden[i] = (float)Math.Tanh(hidden[i]);
        }

        float[] hidden2 = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            hidden2[i] = 0f;
            for (int j = 0; j < hiddenSize; j++)
            {
                hidden2[i] += hidden[j] * hiddenToHiddenWeights[j, i];
            }

            hidden2[i] = (float)Math.Tanh(hidden2[i]);
        }

        float[] outputs = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputs[i] = 0f;
            for (int j = 0; j < hiddenSize; j++)
            {
                outputs[i] += hidden2[j] * hiddenToOutputWeights[j, i];
            }

            outputs[i] = (float)Math.Tanh(outputs[i]);
        }

        return outputs;
    }

    public Brain CloneWithMutation(float mutationRate = 0.4f)
    {
        Brain clone = new Brain();

        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                clone.inputToHiddenWeights[i, j] = MutateWeight(inputToHiddenWeights[i, j], mutationRate);
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                clone.hiddenToHiddenWeights[i, j] = MutateWeight(hiddenToHiddenWeights[i, j], mutationRate);
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                clone.hiddenToOutputWeights[i, j] = MutateWeight(hiddenToOutputWeights[i, j], mutationRate);
            }
        }

        return clone;
    }

    private float MutateWeight(float weight, float mutationRate)
    {
        if (UnityEngine.Random.value < mutationRate)
        {
            float multiplier = 1f + UnityEngine.Random.Range(-0.05f, 0.05f);
            return weight * multiplier;
        }

        return weight;
    }
}