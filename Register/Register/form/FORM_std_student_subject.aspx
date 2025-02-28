<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="FORM_std_student_subject.aspx.cs" Inherits="Register.form.FORM_std_student_subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="flex flex-col items-center w-full p-6">
       
        <label for="stdList" class="text-gray-700 font-medium mb-2">Select Standard:</label>
        <asp:DropDownList ID="stdList" runat="server" AutoPostBack="true" 
            OnSelectedIndexChanged="stdList_SelectedIndexChanged"
            CssClass="border border-gray-300 rounded-lg p-3 w-1/2 focus:ring-2 focus:ring-blue-400 shadow-md">
        </asp:DropDownList>

       
        <div class="w-full mt-6">
           <asp:GridView ID="grid_students" runat="server" AutoGenerateColumns="False" DataKeyNames="id"
                OnRowCommand="grid_students_RowCommand" CssClass="w-full border border-gray-300 rounded-lg shadow-md">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" Visible="false" />
                    <asp:BoundField DataField="student_name" HeaderText="Student Name" />
                    <asp:BoundField DataField="student_email" HeaderText="Email" />
                    <asp:BoundField DataField="student_mobile" HeaderText="Mobile" />

                  
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="btnExpand" runat="server" Text="+" CommandName="ExpandSubjects"
                                CommandArgument="<%# Container.DataItemIndex %>"
                                CssClass="px-4 py-2 rounded-lg bg-blue-400 font-bold hover:bg-blue-600 cursor-pointer" />
                        </ItemTemplate>
                    </asp:TemplateField>

                 
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:GridView ID="grid_subjects" runat="server" AutoGenerateColumns="False" Visible="false"
                                CssClass="w-full mt-2 border border-gray-300 rounded-lg shadow-md">
                                <Columns>
                                    <asp:BoundField DataField="subject_id" HeaderText="ID" Visible="false" />
                                    <asp:TemplateField HeaderText="Subject Name">
                                        <ItemTemplate>
                                            <span class="font-semibold"><%# Container.DataItemIndex + 1 %>. <%# Eval("subject_name") %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                             
                                    <asp:TemplateField HeaderText="Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMarks" runat="server" CssClass="border p-2 rounded-lg w-20"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="text-red-600 font-medium mt-4"></asp:Label>

    </div>

</asp:Content>
