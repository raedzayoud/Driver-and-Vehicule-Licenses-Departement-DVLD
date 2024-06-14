# DVLD - Driving and Vehicles Licenses Development

DVLD is a .NET Desktop Application structured with a Three-tier Architecture, seamlessly integrated with a Microsoft SQL Server Database. It utilizes the capabilities of .NET Windows Form technology, written in C# programming language, and follows the principles of Object-Oriented Programming (OOP).

## Key Features

- **Login Logs**: Records login activities for users.
- **License Classes**: Manage different license classes, allowing applicants to apply for specific categories such as Ordinary driving license, Small motorcycle, Commercial, and more.
- **Testing Procedures**: Schedule and administer three essential tests - vision test, written test, and street test - ensuring applicants meet the necessary criteria for license issuance.
- **User/Drivers Database**: Maintain a centralized database of users and drivers, including details of drivers and individuals applying for various services.
- **License Applications**:
  - **First-Time Application**: Streamlined process for applicants seeking their initial driving license.
  - **Renewal Application**: Simplified renewal process for existing license holders.
  - **Replacement Application**: Efficient handling of requests for replacing damaged or lost licenses.
  - **Retake Tests Application**: Enable applicants to reapply for tests in case of previous failures.
  - **International License Application**: Facilitate the application process for international driving licenses.
  - **Release Detained License Application**: Users can request the release of their detained license, subject to the payment of fines.

## Workflow Overview

1. **Application Submission**: Users can submit applications for different services through the system, specifying the type of service they require.
2. **Test Scheduling**: Schedule vision, written, and street tests for applicants based on their license class and requirements.
3. **Test Evaluation**: Efficiently evaluate and record the results of each test, determining whether the applicant has successfully passed or needs further action.
4. **License Issuance**: Upon successful completion of all tests, the system gives users access to the process of issuing the license.
5. **Detention Feature**: Users have the ability to detain the driver license, initiating a process for payment of fines.
6. **Release Application**: To lift the detention, applicants apply for a release detained license application, paying the required fines.
7. **License Reactivation**: Upon successful payment, the system activates the license, making it operational again.
8. **Database Maintenance**: Continuously update and maintain databases of People, Users, Drivers, Detained licenses, License classes, and Test results for accurate record-keeping.

## Admin Access

The application includes an admin user with default credentials for testing purposes:

- **Username**: `raed`
- **Password**: `1234`

## ## Database Schema

