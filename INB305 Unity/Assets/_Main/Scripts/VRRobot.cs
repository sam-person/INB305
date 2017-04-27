using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using UnityEngine;

public static class VRRobot {

    // ----------------------------------------------------------
    // VRRobot Control Class
    // Used in QUT's Robotronica VR controlled robot project
    // For: QUT Game Development & QUT Robotics societies
    // Written: Jonn Dillon
    // Version: 1.1
    // Version Info: Adjustment methods added and log messages updated
    // Last Changed: 5/3/17
    // ----------------------------------------------------------

    // Variables
    // ----------------------------------------------------------

    //Connection Specific
    private static string connectionIP = "127.0.0.1"; //Default to local
    private static bool hasValidConnection = false;

    //Car Specific
    private static float carDirection = 0.0f; //Negative = degrees left, Postive = degrees right
    private static float carSpeed = 0.0f; //x amounts of units per second

    //Debug Specific
    private static bool isDebugMode = false;
    private static string debugPrefix = "[VRRobot]";

    //private static string ipRegexValidation = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

    // Connection
    // ----------------------------------------------------------

    //Get the connection IP
    public static string ip() {
        debugPrint("Connection IP Grabbed");
        return connectionIP;
    }

    //Set the connection IP
    //Return if setting IP was sucessful
    public static bool ip(string _ip) {
        if (isValidIP(_ip))
        {
            debugPrint("Connection IP Set");
            debugPrint("New IP is " + _ip);
            connectionIP = _ip;
            return true;
        }
        else
        {
            Debug.LogError(debugPrefix + " Given IP is incorrect. Please check your IP construction.");
            return false;
        }
    }

    //Connect to robot based on previously assigned IP
    //Return true if connection sucessful
    public static bool connect() {
        debugPrint("Connecting to " + connectionIP);
        hasValidConnection = true;
        return true;
    }

    //Connect to robot based on given IP, and store
    //Return true if connection sucessful
    public static bool connect(string _ip) {
        if (isValidIP(_ip))
        {
            ip(_ip); //Sets the IP
            if (connect(_ip))
            {
                debugPrint("Connection was sucessful");
                return true;
            }
            else
            {
                debugPrint("Connection was unsucessful");
                return false;
            }
        }
        else
        {
            debugPrint("Failed to connect with designated IP");
            return false;
        }

    }

    // Robot Control
    // ----------------------------------------------------------

    //Get the car's last direction
    //Will assume you want the previously set direction, rather than the live version
    public static float direction() {
        debugPrint("Returnning last set direction");
        return carDirection;
    }

    //*** THIS IS UNLIKELY TO IMPLEMENTED ***
    //Get the car's last direction
    public static float direction(bool isLive) {
        if (isLive)
        {
            if (hasValidConnection)
            {
                debugPrint("Getting live direction");
                //Get the live direction
                debugPrint("Returning live direction");
                return direction();
            }
        }
        debugPrint("Connection not live, returning last set direction");
        return direction();

    }

    //Sets the direction the robot its going
    public static void direction(float _dir) {
        debugPrint("Setting direction and updating");
        updateCarDirection(_dir);
    }

    //Adds then sets the direction the robot is going
    public static void directionAdjust(float _dir) {
        debugPrint("Setting direction and updating");
        updateCarDirection(_dir + carDirection);
    }


        
    //Get the car's last direction
    //Will assume you want the previously set direction, rather than the live version
    public static float speed() {
        debugPrint("Returnning last set speed");
        return carSpeed;
    }

    //*** THIS IS UNLIKELY TO IMPLEMENTED ***
    //Get the car's last direction
    public static float speed(bool isLive) {
        if (isLive)
        {
            if (hasValidConnection)
            {
                debugPrint("Getting live speed");
                //Get the live speed
                debugPrint("Returning live speed");
                return speed();
            }
        }
        debugPrint("Connection not live, returning last set speed");
        return speed();
    }

    //Sets the speed the robot is going
    public static void speed(float _speed) {
        debugPrint("Setting speed and updating");
        updateCarSpeed(_speed);
    }

    //Adds then sets the speed the robot is going
    public static void speedAdjust(float _speed) {
        debugPrint("Setting speed and updating");
        updateCarSpeed(_speed + carSpeed);
    }

    // Connection
    // ----------------------------------------------------------

    private static void updateCarSpeed(float _speed) {
        carSpeed = _speed;
        //Do Connection Stuff and Transmission here
    }

    private static void updateCarDirection(float _direction) {
        carDirection = _direction;
        //Do Connection Stuff and Transmission here
    }

    // Utilities
    // ----------------------------------------------------------

    //Prints all related info to the log
    public static void printInfo() {
        Debug.Log(debugPrefix + " IP: " + connectionIP + " | Car Direction: " + carDirection + " | Car Speed: " + carSpeed);
    }

    //Disable logging to console
    public static void debugMode(bool _setMode) {
        isDebugMode = _setMode;
    }

    //Prints any debug message, while checking if the user has allowed it
    private static void debugPrint(string _content) {
        if (isDebugMode)
        {
            Debug.Log("" + debugPrefix + " " + _content);
        }
    }

    //Checks the validity of the given IP. To prevent the program having major issues.
    private static bool isValidIP(string _ip) {
        IPAddress ip;
        if (IPAddress.TryParse(_ip, out ip))
        {
            debugPrint("IP Passed Sanatisation");
            return true;
        }
        else
        {
            debugPrint("IP Failed Sanatisation");
            return false;
        }
    }
}
