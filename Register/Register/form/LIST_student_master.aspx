<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/form/FORM.Master" CodeBehind="LIST_student_master.aspx.cs" Inherits="Register.form.LIST_studet_master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex items-center flex-col">
        <div class="text-3xl text-black font-bold flex items-center gap-10 mb-10">
            Student Details
            <asp:Button runat="server" ID="addStd" 
                class="text-white text-xl font-bold px-3 py-2 rounded-lg bg-blue-500 hover:bg-blue-600 cursor-pointer" 
                Text="Add more" OnClick="addStd_Click"/>
        </div>

        <asp:GridView runat="server" ID="grid_stud" OnRowCommand="grid_stud_RowCommand"
            CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px" DataKeyNames="id"
             AutoGenerateColumns="false" CssClass="my-crystal-report w-full max-w-6xl table-auto border border-gray-300">
            
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#007bff" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />

            <Columns>
            
                <asp:BoundField DataField="id" HeaderText="Id" Visible="false"/>
                <asp:BoundField DataField="first_name" HeaderText="First name" />
                <asp:BoundField DataField="middle_name" HeaderText="Middle name" />
                <asp:BoundField DataField="last_name" HeaderText="Last name" />
                <asp:BoundField DataField="student_email" HeaderText="Email" />
                <asp:BoundField DataField="student_mobile" HeaderText="Mobile" />
                <asp:BoundField DataField="student_gender" HeaderText="Gender" />
                <asp:BoundField DataField="student_city" HeaderText="City" />
                <asp:BoundField DataField="student_address" HeaderText="Address" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <div class="flex gap-3 whitespace-nowrap">
                        <asp:Button ID="btn_edit" runat="server" OnCommand="btn_edit_Command" CommandName="edit_student" 
                            Text="Edit" CommandArgument='<%#Eval("id") %>'

                            CssClass="btn-edit px-4 py-2 rounded-lg bg-green-400 font-bold hover:bg-green-600 cursor-pointer mr-5"/>
                        <asp:Button ID="del_edit" runat="server" CommandName="delete_data" 
                            Text="Delete" CommandArgument='<%#Eval("id")%>'
                           CssClass="btn-delete px-4 py-2 rounded-lg bg-red-400 font-bold hover:bg-red-600 cursor-pointer"/>
                            </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label runat="server" ID="lbl1"></asp:Label>
    </div>
</asp:Content>
