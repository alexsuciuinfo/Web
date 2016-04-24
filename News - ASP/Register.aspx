<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    

    <asp:CreateUserWizard LoginCreatedUser="true" OnFinishButtonClick="CreateUserWizard1_FinishButtonClick" ID="CreateUserWizard1" runat="server" OnContinueButtonClick="CreateUserWizard1_ContinueButtonClick" OnCreatedUser="CreateUserWizard1_CreatedUser" CompleteSuccessTextStyle-ForeColor="Green">
        
        <WizardSteps>

           <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
    <ContentTemplate>
        <table border="0" style="font-size: 100%; font-family: Verdana">
            <tr>
                <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #5d7b9d">
                    Sign Up for Your New Account</td>
            </tr>
             
            <tr>
                <td align="right">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                        User Name:</asp:Label></td>
                <td>
                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">
                        Password:</asp:Label></td>
                <td>
                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">
                        Confirm Password:</asp:Label></td>
                <td>
                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                        ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">
                        E-mail:</asp:Label></td>
                <td>
                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                        ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regexpName" runat="server"     
                                    ErrorMessage="Email is not valid" 
                                    ControlToValidate="Email"
                                    ValidationGroup="CreateUserWizard1"     
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="QuestionLabel" runat="server" AssociatedControlID="Question">
                        Security Question:</asp:Label></td>
                <td>
                    <asp:TextBox ID="Question" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="QuestionRequired" runat="server" ControlToValidate="Question"
                        ErrorMessage="Security question is required." ToolTip="Security question is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">
                        Security Answer:</asp:Label></td>
                <td>
                    <asp:TextBox ID="Answer" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                        ErrorMessage="Security answer is required." ToolTip="Security answer is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                        ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                        ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="color: red">
                    <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:CreateUserWizardStep>
            <asp:WizardStep>
                <table border="0" style="font-size: 100%; font-family: Verdana">
            <tr>
                <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #5d7b9d">
                    Personal data</td>
            </tr>
               <tr>
                <td align="right">
                    <asp:Label ID="FNameLabel" runat="server" AssociatedControlID="FName">
                        First Name:</asp:Label></td>
                <td>
                    <asp:TextBox ID="FName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FNameRequired" runat="server" ControlToValidate="FName"
                        ErrorMessage="First Name is required" ToolTip="First Name is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
               <tr>
                <td align="right">
                    <asp:Label ID="LNameLabel" runat="server" AssociatedControlID="LName">
                        Last Name:</asp:Label></td>
                <td>
                    <asp:TextBox ID="LName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="LNameRequired" runat="server" ControlToValidate="LName"
                        ErrorMessage="Last Name is required" ToolTip="Last Name is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
                         <tr>
                <td align="right">
                    <asp:Label ID="BirthDateLabel" runat="server" AssociatedControlID="BirthDate">
                        Birth Date:</asp:Label></td>
                <td>
                    <asp:TextBox TextMode="Date" ID="BirthDate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="BirthDateRequired" runat="server" ControlToValidate="BirthDate"
                        ErrorMessage="BirthDate is required" ToolTip="BirthDate is required."
                        ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                </td>
            </tr>
                      
                         <tr>
                <td align="right">
                    <asp:Label ID="PhoneLabel" runat="server" AssociatedControlID="Phone">
                        Phone:</asp:Label></td>
                <td>
                    <asp:TextBox TextMode="Phone" ID="Phone" runat="server"></asp:TextBox>
                </td>
                
            </tr>

                     </tr>
                      
                         <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="Phone">
                        Website:</asp:Label></td>
                <td>
                    <asp:TextBox  ID="WebSite" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            </asp:WizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
   
    <asp:Literal ID="LRaspuns" runat="server" />

    </asp:content>

