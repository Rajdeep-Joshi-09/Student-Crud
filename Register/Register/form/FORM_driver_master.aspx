<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="FORM_driver_master.aspx.cs" Inherits="Register.form.FORM_driver_master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex flex-col items-center gap-3 rounded-lg bg-white shadow-2xl p-8 w-[35%] border border-gray-300">
    <div class="text-3xl font-bold text-gray-800 mb-3">
            Driver Form
        </div>

        <asp:TextBox runat="server" ID="txt_driver_name" placeholder="Enter Driver name" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="txt_driver_name" ErrorMessage="Driver name is required" ForeColor="Red"></asp:RequiredFieldValidator>
           
         <asp:TextBox runat="server" ID="txt_driver_number"  placeholder="Enter Driver Mobile Number" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rv2" runat="server" ControlToValidate="txt_driver_number" ErrorMessage="Mobile Number is required" ForeColor="Red"></asp:RequiredFieldValidator>

     <asp:Button runat="server" ID="subBtn" OnClick="subBtn_Click" Text="Submit" CssClass="bg-blue-600 text-white font-bold text-lg px-5 py-3 hover:bg-blue-800 cursor-pointer rounded-lg mt-3 shadow-md" />
        <asp:Label runat="server" ID="lbl1" ></asp:Label>
        </div>
</asp:Content>
