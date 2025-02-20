<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="LIST_student_subject.aspx.cs" Inherits="Register.form.LIST_student_subject" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="flex items-center flex-col">
    <div class="text-3xl text-black font-bold flex items-center gap-10 mb-10">
            Student wise subject Details
            <asp:Button runat="server" ID="btnSS" onClick="btnSS_Click" class="text-white text-xl font-bold px-3 py-2 rounded-lg bg-blue-500 hover:bg-blue-600 cursor-pointer" Text="Add more" />
        </div>

        <asp:GridView runat="server" ID="grid_ss" CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px"  AutoGenerateColumns="false">
            <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />

            <Columns>
                <asp:BoundField DataField="ss_is" HeaderText="id" Visible="false" />
                <asp:BoundField DataField="ss_subject_id" HeaderText="Subject name" />
                <asp:BoundField DataField="ss_student_id" HeaderText="Student name" />
          
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btn_edit" runat="server" Text="Edit" CommandName="edit_btn" OnCommand="btn_edit_Command" CommandArgument='<%#Eval("ss_id") %>' CssClass="px-4 py-2 rounded-lg bg-green-400 font-bold hover:bg-green-600 cursor-pointer mr-5" />
                        <asp:Button ID="del_edit" CommandName="del_btn" runat="server" Text="Delete" OnCommand="del_edit_Command" CommandArgument='<%#Eval("ss_id") %>' CssClass="px-4 py-2 rounded-lg bg-red-400 font-bold hover:bg-red-600 cursor-pointer" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <asp:Label runat="server" ID="lbl1" ></asp:Label>
        </div>
</asp:Content>
