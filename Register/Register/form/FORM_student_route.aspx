<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="FORM_student_route.aspx.cs" Inherits="Register.form.FORM_student_route" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="flex flex-col items-center gap-3 rounded-lg bg-white shadow-2xl p-8 w-[35%] border border-gray-300">

        <div class="text-3xl font-bold text-gray-800 mb-3">
            Student-Route Form
        </div>

         <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select BusRouteDriver:</p>
            <asp:DropDownList ID="routeList" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
      
            </asp:DropDownList>
            <asp:Label runat="server" ID="l1" ForeColor="red"></asp:Label>
        </div>  
        
        <div class="flex flex-col items-center w-full">
            <p class="text-gray-700 font-medium">Select Stops:</p>
            <asp:DropDownList ID="stopList" runat="server" CssClass="border border-gray-300 rounded-lg p-3 w-3/4 focus:ring-2 focus:ring-blue-400">
               
            </asp:DropDownList>
               <asp:Label runat="server" ID="l2" ForeColor="red"></asp:Label> 

        </div>
        
          


        <asp:Button runat="server" OnClick="submitBtn_Click" ID="submitBtn" Text="Submit" cssClass="bg-blue-600 text-white font-bold text-lg px-5 py-3 hover:bg-blue-800 cursor-pointer rounded-lg mt-3 shadow-md"/>
        <asp:Label runat="server" ID="lbl1"></asp:Label>

    </div>
</asp:Content>
