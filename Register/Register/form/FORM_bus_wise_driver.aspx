<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="FORM_bus_wise_driver.aspx.cs" Inherits="Register.form.FORM_bus_wise_driver" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex flex-col items-center gap-3 rounded-lg bg-white shadow-2xl p-8 w-[35%] border border-gray-300">
        <div class="text-3xl font-bold text-gray-800 mb-3">
            Bus Wise Driver Assignment
        </div>

        <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select Bus:</p>
            <asp:DropDownList ID="busList" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
                
      
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rvBus" runat="server" ControlToValidate="busList" InitialValue="0" ErrorMessage="Bus is required" ForeColor="Red"></asp:RequiredFieldValidator>

        <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select Route:</p>
            <asp:DropDownList ID="routeList" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
              
             
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rvRoute" runat="server" ControlToValidate="routeList" InitialValue="0" ErrorMessage="Route is required" ForeColor="Red"></asp:RequiredFieldValidator>

      
        <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select Driver:</p>
            <asp:DropDownList ID="driverList" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
                
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="rvDriver" runat="server" ControlToValidate="driverList" InitialValue="0" ErrorMessage="Driver is required" ForeColor="Red"></asp:RequiredFieldValidator>

      
        <asp:Button runat="server" ID="submitBtn" Text="Submit" CssClass="bg-blue-600 text-white font-bold text-lg px-5 py-3 hover:bg-blue-800 cursor-pointer rounded-lg mt-3 shadow-md" OnClick="subBtn_Click" />

        <asp:Label runat="server" ID="lbl1"></asp:Label>
    </div>
</asp:Content>
