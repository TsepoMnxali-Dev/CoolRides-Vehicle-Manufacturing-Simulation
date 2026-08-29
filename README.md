# 🚗 CoolRides — Vehicle Manufacturing System

> **A multi-threaded vehicle manufacturing simulation built with C# and classic Design Patterns.**

CoolRides simulates a manufacturing environment where **Cars** and **Minibuses** are produced through dedicated assembly lines and processed through a **shared spray booth**.

The system demonstrates how multiple **Gang of Four (GoF) Design Patterns** can work together to create a structured, scalable, and thread-safe manufacturing pipeline.

![image alt](https://github.com/TsepoMnxali-Dev/CoolRides-Vehicle-Manufacturing-Simulation/blob/72fcca5f4c1f8b9829779b8561d34d250b353931/CoolRides.png)

## 🎥 Demo

<p align="center">
  <a href="https://www.dropbox.com/scl/fi/upf7cpb86rn0dontlo8bq/CoolRides-Vehicle-Manufacturing-System-Demo.mov?rlkey=vl66r4flgdgetgbosdjczuyzl&st=lq0jha0l&dl=0">
    ▶️ <strong>Watch the CoolRides Demo</strong>
  </a>
</p>

---

## 🏭 System Overview

CoolRides consists of:

* 🚗 **Car Assembly Line**
* 🚌 **Minibus Assembly Line**
* 🎨 **Shared Spray Booth**
* 🏢 **Corporate HQ**
* 🖥️ **GUI for placing vehicle orders**
* ⚙️ **Background processing and command queues**
* 🧩 **Vehicle-specific component factories**

Orders are placed through the GUI and sent to **Corporate HQ**, where they are queued and processed sequentially. The appropriate assembly line then constructs the requested vehicle using compatible components before sending it to the shared spray booth.

### Manufacturing Flow

```text
                    ┌─────────────────┐
                    │       GUI       │
                    │  Place Order    │
                    └────────┬────────┘
                             │
                             ▼
                    ┌─────────────────┐
                    │  Corporate HQ   │
                    │ Command Queue   │
                    └────────┬────────┘
                             │
                    ┌────────┴────────┐
                    │                 │
                    ▼                 ▼
             ┌──────────────┐  ┌──────────────┐
             │ Car Assembly │  │   Minibus    │
             │     Line     │  │ Assembly Line│
             └──────┬───────┘  └──────┬───────┘
                    │                 │
                    ▼                 ▼
             ┌──────────────┐  ┌──────────────┐
             │  Car Parts   │  │ Minibus Parts│
             │   Factory    │  │   Factory    │
             └──────┬───────┘  └──────┬───────┘
                    │                 │
                    └────────┬────────┘
                             ▼
                    ┌─────────────────┐
                    │  Shared Spray   │
                    │      Booth      │
                    │   (Singleton)   │
                    └─────────────────┘
```

---

# 🧩 Design Patterns

The project combines **four design patterns** to separate responsibilities and control the manufacturing process.

---

## 📦 1. Command Pattern

The **Command Pattern** converts vehicle orders into objects that can be queued, managed, and executed sequentially.

This allows Corporate HQ to keep track of incoming orders while assembly lines process them asynchronously.

### Components

| Component           | Responsibility                                              |
| ------------------- | ----------------------------------------------------------- |
| **Client**          | GUI where users select the vehicle type and colour          |
| **Invoker**         | Corporate HQ; manages command queues and background threads |
| **ICommand**        | Defines the command contract                                |
| **ConcreteCommand** | Stores the assembly line and requested vehicle colour       |
| **Receiver**        | Assembly Line that performs the manufacturing process       |

### Why Command?

Instead of directly telling an assembly line to build a vehicle, the request becomes an object:

```text
GUI
 │
 ▼
Vehicle Order
 │
 ▼
Command
 │
 ▼
HQ Queue
 │
 ▼
Assembly Line
```

This provides a clean separation between **requesting an operation** and **executing the operation**.

---

# 🏗️ 2. Factory Method Pattern

CoolRides has separate assembly lines for different vehicle types.

The **Factory Method Pattern** allows the system to determine which vehicle should be created without tightly coupling the manufacturing process to a specific vehicle class.

### Structure

```text
             FactoryBase
                  │
        ┌─────────┴─────────┐
        ▼                   ▼
 CarAssemblyLine    MinibusAssemblyLine
        │                   │
        ▼                   ▼
      Car()              MiniBus()
```

### Main Components

**ProductBase**

Defines the common vehicle production workflow:

```text
Request Parts
     ↓
Wait for Assembly Time
     ↓
Assemble Vehicle
     ↓
Send to Spray Booth
```

**FactoryBase**

Defines the factory structure and vehicle creation process.

**Concrete Factories**

* `CarAssemblyLine`
* `MinibusAssemblyLine`

Each factory overrides the factory method to create its appropriate vehicle.

### Why Factory Method?

The Command Pattern can trigger the appropriate assembly line without needing to know how the vehicle itself is constructed.

---

# 🧱 3. Abstract Factory Pattern

Cars and Minibuses require **different families of components**.

For example:

```text
Car
├── Car Chassis
├── Car Shell
├── Car Wheels
└── Car Trim

Minibus
├── Minibus Chassis
├── Minibus Shell
├── Minibus Wheels
└── Minibus Trim
```

The **Abstract Factory Pattern** guarantees that an assembly line receives components belonging to the correct vehicle family.

### Abstract Factory

Defines methods such as:

```text
CreateChassis()
CreateShell()
CreateWheel()
CreateTrim()
```

### Concrete Factories

```text
Abstract Factory
       │
 ┌─────┴─────────┐
 ▼               ▼
CarPartsFactory  MinibusPartsFactory
```

Each concrete factory creates compatible components for its vehicle type.

### Product Interfaces

The system defines abstract product interfaces such as:

* `IChassis`
* `IShell`
* `IWheel`
* `ITrim`

The assembly line works with these abstractions rather than depending directly on concrete component classes.

### ⏱️ Manufacturing Delays

The factories also simulate different manufacturing times.

For example:

```text
CarPartsFactory
    └── CreateTrim() → 1 second

MinibusPartsFactory
    └── CreateTrim() → 2 seconds
```

This makes the simulation behave more like a real manufacturing environment.

---

# 🔐 4. Singleton Pattern

The factory floor contains a **single shared spray booth**.

The Singleton Pattern ensures that only **one SprayBooth instance** exists throughout the application.

```text
          Assembly Line
                │
                ▼
        ┌───────────────┐
        │   SprayBooth  │
        │   Singleton   │
        └───────────────┘
                ▲
                │
          Assembly Line
```

### Thread Safety

Because multiple assembly lines can operate simultaneously, the Singleton implementation uses **double-check locking** to safely create the shared instance.

This prevents multiple spray booths from being created when different assembly-line threads request access at the same time.

The spray booth therefore acts as a **shared resource** for the manufacturing system.

---

# ⚙️ Multi-Threading

One of the key features of CoolRides is its use of **background threads**.

Corporate HQ manages the order queues while assembly lines operate independently.

This allows the simulation to represent multiple manufacturing processes happening concurrently.

```text
                 Corporate HQ
                      │
              ┌───────┴───────┐
              │               │
         Thread 1        Thread 2
              │               │
              ▼               ▼
       Car Assembly     Minibus Assembly
              │               │
              └───────┬───────┘
                      ▼
                Shared Resource
                 Spray Booth
```

The Singleton Spray Booth provides a single shared resource that must safely handle requests from multiple threads.

---

# 🛠️ Tech Stack

| Technology                      | Purpose                             |
| ------------------------------- | ----------------------------------- |
| **C#**                          | Core programming language           |
| **.NET**                        | Application framework               |
| **Object-Oriented Programming** | System architecture                 |
| **Design Patterns**             | Software design and maintainability |
| **Multi-threading**             | Concurrent manufacturing processes  |
| **GUI**                         | Vehicle order interface             |

---

# 🧠 Design Pattern Interaction

The real strength of CoolRides comes from how the patterns **work together**, rather than operating independently.

```text
                 ┌───────────┐
                 │    GUI    │
                 └─────┬─────┘
                       │
                  COMMAND
                       │
                       ▼
               ┌──────────────┐
               │ Corporate HQ │
               └──────┬───────┘
                      │
              ┌───────┴────────┐
              ▼                ▼
       Factory Method    Factory Method
              │                │
              ▼                ▼
        Car Factory      Minibus Factory
              │                │
              ▼                ▼
       Abstract Factory  Abstract Factory
              │                │
              └───────┬────────┘
                      ▼
                SINGLETON
                Spray Booth
```

Each pattern has a specific responsibility:

* **Command** → Manages and queues orders
* **Factory Method** → Determines which vehicle is produced
* **Abstract Factory** → Creates compatible vehicle components
* **Singleton** → Controls access to the shared spray booth

---

# 🎯 Project Objective

The objective of CoolRides is to demonstrate how **software design patterns, object-oriented programming, and multi-threading** can be combined to model a realistic manufacturing environment.

Rather than building the system as one tightly coupled application, responsibilities are separated into reusable and maintainable components.

---

# 🚀 Key Concepts Demonstrated

* ✅ Command Pattern
* ✅ Factory Method Pattern
* ✅ Abstract Factory Pattern
* ✅ Singleton Pattern
* ✅ Object-Oriented Design
* ✅ Encapsulation
* ✅ Abstraction
* ✅ Polymorphism
* ✅ Interfaces
* ✅ Multi-threading
* ✅ Thread-safe resource management
* ✅ Producer/consumer-style command queues
* ✅ Separation of responsibilities

---

# 📌 Conclusion

**CoolRides** demonstrates how multiple design patterns can be combined to create a cohesive software architecture.

The GUI initiates a **Command**, which is placed into the Corporate HQ queue. The command is then processed by the appropriate **Factory Method** assembly line. The assembly line uses an **Abstract Factory** to obtain the correct vehicle components before the completed vehicle is sent to the shared **Singleton Spray Booth**.

The result is a manufacturing simulation where each design pattern has a clear responsibility while working together as one complete system.

> **Four patterns. Two assembly lines. One spray booth. One manufacturing pipeline.**

---

## 👨‍💻 Project

**CoolRides — Vehicle Manufacturing Simulation**

Built to demonstrate practical application of software design patterns in a multi-threaded C# environment.
