# Sharp-Shooters

# Sharp_Shooters Banking System

## Overview

This C# banking system, named Sharp_Shooters, is designed to provide users with a simple and secure platform for managing their accounts, performing transactions, and borrowing money. The system features a user-friendly interface and includes functionalities for both regular customers and administrators.

## Table of Contents

- [Classes](#classes)
  - [1. Accounts](#1-accounts)
  - [2. Admin](#2-admin)
  - [3. Bank](#3-bank)
  - [4. Currency](#4-currency)
  - [5. Program](#5-program)
  - [6. TransferData](#6-transferdata)
  - [7. User](#7-user)
  - [8. Utility](#8-utility)
- [Getting Started](#getting-started)
- [Features](#features)
- [Usage](#usage)
- [Contributors](#contributors)

## Classes

### 1. Accounts

- **Description:**
  The `Accounts` class manages user bank accounts, including creation, deletion, and overview functionalities. It supports opening regular and savings accounts.

### 2. Admin

- **Description:**
  The `Admin` class handles administrative tasks, allowing admins to log in, manage user accounts, and perform various administrative functions.

### 3. Bank

- **Description:**
  The `Bank` class serves as the main entry point for the banking system. It provides a welcome menu, user login functionality, and the main menu for customers and admins.

### 4. Currency

- **Description:**
  The `Currency` class is responsible for managing currency exchange rates, handling money borrowing, calculating loan limits, and performing currency conversions.

### 5. Program

- **Description:**
  The `Program` class contains the `Main` method and initializes and runs the banking system. It calls the `Run` method from the `Bank` class.

### 6. TransferData

- **Description:**
  The `TransferData` class encapsulates information required for money transfers, including sender, recipient, source account, destination account, and transfer amount.

### 7. User

- **Description:**
  The `User` class defines user properties such as username, PIN code, accounts, transaction history, and initial total balance. It includes a secure login method.

### 8. Utility

- **Description:**
  The `Utility` class provides utility methods for improving the user interface, including displaying error messages, waiting for Enter key presses, and hiding PIN codes during input.

## Getting Started

1. **Run the Program:**
   Execute the `Main` method in the `Program` class to start the banking system.

2. **Log in:**
   Choose between customer and admin login. Users can perform various banking operations, while admins have additional administrative functionalities.

3. **Explore Features:**
   Explore features such as managing accounts, transferring money, viewing transaction history, and borrowing money.

4. **Exit:**
   Use the main menu options to log out or exit the application.

## Features

- User-friendly interface
- Secure user login with lockout mechanism
- Account management (creation, deletion, and overview)
- Money transfer between accounts
- Currency exchange and conversion
- Loan borrowing with interest rate calculation
- Administrative functionalities for system management

## Usage

Follow the instructions provided in the [Getting Started](#getting-started) section to run the application. Explore the different features available for both regular customers and administrators.

## Contributors

- [Eddanz](https://github.com/Eddanz)
- [Simonnilsson9](https://github.com/simonnilsson9)
- [Waltersdubbelnugge](https://github.com/waltersdubbelnugge)
- [Kamelen123](https://github.com/Kamelen123)

Link to UML: https://t.ly/gTMZA
