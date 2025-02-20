<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="LIST_subject_master.aspx.cs" Inherits="Register.form.LIST_subject_master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="flex items-center flex-col">
        <div class="text-3xl text-black font-bold flex items-center gap-10 mb-10">
            Subject Details
            <asp:Button runat="server" ID="addSub" 
                class="text-white text-xl font-bold px-3 py-2 rounded-lg bg-blue-500 hover:bg-blue-600 cursor-pointer" 
                Text="Add more" OnClick="addSub_Click"/>
        </div>

        <asp:GridView runat="server" ID="grid_sub" 
            CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px"
            AutoGenerateColumns="false" CssClass="my-crystal-report">
            
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
                <asp:BoundField DataField="subject_id" HeaderText="ID" Visible="false" />
                <asp:BoundField DataField="subject_name" HeaderText="Subject Name" />
                <asp:BoundField DataField="subject_code" HeaderText="Subject Code" />
                <asp:BoundField DataField="subject_credit" HeaderText="subject Credit" />
                <asp:BoundField DataField="subject_standerd_id" HeaderText="Subject-Standerd" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btn_edit" runat="server" OnCommand="btn_edit_Command" CommandName="edit_student" 
                            Text="Edit" CommandArgument='<%#Eval("subject_id") %>' 
                             CssClass="btn-edit px-4 py-2 rounded-lg bg-green-400 font-bold hover:bg-green-600 cursor-pointer mr-5"/>
                        <asp:Button ID="btn_del" runat="server" CommandName="delete_data" OnCommand="btn_del_Command" 
                            Text="Delete" CommandArgument='<%#Eval("subject_id")%>' 
                             CssClass="btn-delete px-4 py-2 rounded-lg bg-red-400 font-bold hover:bg-red-600 cursor-pointer"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label runat="server" ID="lbl1"></asp:Label>
    </div>
</asp:Content>
