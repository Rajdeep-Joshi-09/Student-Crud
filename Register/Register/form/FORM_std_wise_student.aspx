<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="FORM_std_wise_student.aspx.cs" Inherits="Register.BAL.FORM_std_wise_student" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex flex-col items-center gap-3 rounded-lg bg-white shadow-2xl p-8 w-[35%] border border-gray-300">
        <div class="text-3xl font-bold text-gray-800 mb-3">
            Standerd Wise Student
        </div>

        <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select Standerd:</p>
            <asp:DropDownList ID="stdDetails" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
                
      
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="stdDetails"  ErrorMessage="Standerd is required" ForeColor="Red"></asp:RequiredFieldValidator>

        <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select Student:</p>
            <asp:DropDownList ID="studDetails" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
              
             
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rv2" runat="server" ControlToValidate="studDetails"  ErrorMessage="Student is required" ForeColor="Red"></asp:RequiredFieldValidator>

      
        <asp:Button CausesValidation="true"
 runat="server" ID="submitBtn" OnClick="submitBtn_Click" Text="Submit" CssClass="bg-blue-600 text-white font-bold text-lg px-5 py-3 hover:bg-blue-800 cursor-pointer rounded-lg mt-3 shadow-md"  />

        <asp:Label runat="server" ID="lbl1"></asp:Label>
    </div>
</asp:Content>
