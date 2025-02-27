<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/form/FORM.Master" CodeBehind="~/form/FORM_student_master.aspx.cs" Inherits="Register.form.reg" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="flex flex-col items-center gap-3 rounded-lg bg-white shadow-2xl p-8 w-[35%] border border-gray-300">

        <div class="text-3xl font-bold text-gray-800 mb-3">
            Registration Form
        </div>

        <asp:TextBox runat="server" ID="txtFirstname" placeholder="Enter First name" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="txtFirstname" ErrorMessage="First name is required" ForeColor="Red"></asp:RequiredFieldValidator>

        <asp:TextBox runat="server" ID="txtMiddleName" placeholder="Enter Middle name" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMiddleName" ErrorMessage="Middle Name is required" ForeColor="Red"></asp:RequiredFieldValidator>

        <asp:TextBox runat="server" ID="txtLastName" placeholder="Enter Last name" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name is required" ForeColor="Red"></asp:RequiredFieldValidator>


        <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter your email"
            CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="rv2" runat="server"
            ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <asp:Label runat="server" ID="emailLabel" ForeColor="Red"></asp:Label>
        <!-- RegularExpressionValidator for Email -->
        <asp:RegularExpressionValidator ID="regexEmail" runat="server"
            ControlToValidate="txtEmail"
            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"
            ErrorMessage="Invalid email format"
            ForeColor="Red">
        </asp:RegularExpressionValidator>

        <asp:TextBox runat="server" ID="txtMobile" placeholder="Enter Mobile number"
            CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="rv6" runat="server"
            ControlToValidate="txtMobile" ErrorMessage="Mobile number is required" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <asp:Label runat="server" ID="mobileLabel" ForeColor="Red"></asp:Label>
        <!-- RegularExpressionValidator for Mobile Number -->
        <asp:RegularExpressionValidator ID="regexMobile" runat="server"
            ControlToValidate="txtMobile"
            ValidationExpression="^[6-9]\d{9}$"
            ErrorMessage="Invalid mobile number format"
            ForeColor="Red">
        </asp:RegularExpressionValidator>

        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" placeholder="Enter your address" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rv3" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required" ForeColor="Red"></asp:RequiredFieldValidator>

        <div class="flex gap-5 justify-center w-full">
            <p class="text-gray-700 font-medium">Select Gender:</p>
            <asp:RadioButtonList ID="genderList" runat="server" RepeatDirection="Horizontal" CssClass="flex gap-5 text-gray-700">
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <asp:RequiredFieldValidator ID="rv4" runat="server" ControlToValidate="genderList" InitialValue="" ErrorMessage="Gender is required" ForeColor="Red"></asp:RequiredFieldValidator>

        <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select City:</p>
            <asp:DropDownList ID="cityList" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
                <asp:ListItem Value="0" Text="Please Select Value" />
                <asp:ListItem Value="Rajkot" Text="Rajkot" />
                <asp:ListItem Value="Baroda" Text="Baroda" />
                <asp:ListItem Value="Jamnagar" Text="Jamnagar" />
                <asp:ListItem Value="Ahemdabad" Text="Ahemdabad" />
            </asp:DropDownList>

            <asp:Label runat="server" ID="lblCity"></asp:Label>
        </div>


        <asp:Button runat="server" ID="subBtn" Text="Submit" CssClass="bg-blue-600 text-white font-bold text-lg px-5 py-3 hover:bg-blue-800 cursor-pointer rounded-lg mt-3 shadow-md" OnClick="subBtn_Click" />
        <asp:Label runat="server" ID="lbl1"></asp:Label>
    </div>
</asp:Content>
