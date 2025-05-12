public class Calculator {
    public static void main(String[] args) {
        if (args.length < 3) {
            System.out.println("Usage: java Calculator <num1> <operator> <num2>");
            return;
        }

        double num1 = Double.parseDouble(args[0]);
        String op = args[1];
        double num2 = Double.parseDouble(args[2]);
        double result = 0;
        String str = args[3];

        switch (op) {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 == 0) {
                    System.out.println("Error: Division by zero");
                    return;
                }
                result = num1 / num2;
                break;
            default:
                System.out.println("Invalid operator");
                return;
        }

        
        System.out.println("Calculate result: " + result);
        System.out.println("Reversed String: " + reverseString(str));
    }
    public static String reverseString(String str) {
        String water = "";
        for (int i = str.length() - 1; i >= 0; i--) {
            water += str.charAt(i);
        }
        return water;
    }
}
