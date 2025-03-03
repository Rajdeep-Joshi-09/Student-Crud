<%@ Page Title="" Language="C#" MasterPageFile="~/form/FORM.Master" AutoEventWireup="true" CodeBehind="FORM_std_student_subject.aspx.cs" Inherits="Register.form.FORM_std_student_subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style>
    .outer-container { border: 2px solid #333; padding: 20px; border-radius: 8px; background-color: #fafafa; max-width: 1200px; margin: auto; }
    table { width: 100%; border-collapse: collapse; margin-top: 20px; }
    th, td { border: 1px solid #ccc; padding: 10px 8px; text-align: left; vertical-align: middle; }
    th { background-color: #e0e0e0; font-weight: bold; }
    .subject-row td { border-top: 2px solid #333; border-bottom: 2px solid #333; }
    .subject-container { display: grid; grid-template-columns: repeat(auto-fill, minmax(300px, 1fr)); gap: 20px; align-items: center; padding: 10px 0; }
    .subject-item { display: flex; align-items: center; }
    .subject-item span { margin-right: 10px; font-weight: bold; }
    .mark-textbox { border: 1px solid #aaa; padding: 5px 8px; border-radius: 4px; width: 100px; margin-right: 10px; }
    .expand-btn { background: linear-gradient(to bottom, #4a90e2, #357ab8); color: #fff; border: none; border-radius: 4px; padding: 5px 12px; cursor: pointer; font-weight: bold; transition: background 0.3s ease; }
    .expand-btn:hover { background: linear-gradient(to bottom, #357ab8, #4a90e2); }
    .std-dropdown { padding: 8px; width: 220px; border: 1px solid #ccc; border-radius: 4px; margin-bottom: 20px; }
    .global-submit-button { background: linear-gradient(to bottom, #ff9800, #e68a00); color: #fff; border: none; border-radius: 6px; padding: 8px 16px; cursor: pointer; font-weight: bold; transition: background 0.3s ease; margin-top: 20px; }
    .global-submit-button:hover { background: linear-gradient(to bottom, #e68a00, #ff9800); }
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" />
  
  <div class="outer-container">
    <!-- Standard Dropdown -->
    <div style="margin-bottom:20px;">
      <label style="font-weight:bold;">Select Standard:</label>
      <asp:DropDownList ID="stdList" runat="server" AutoPostBack="true" 
          OnSelectedIndexChanged="stdList_SelectedIndexChanged" CssClass="std-dropdown">
      </asp:DropDownList>
    </div>
    
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" style="display:block; margin-bottom:10px;"></asp:Label>
    
    <!-- Students Table -->
    <table>
      <thead>
        <tr>
          <th>Student Name</th>
          <th>Email</th>
          <th>Mobile</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        <asp:Repeater ID="rptStudents" runat="server" 
          OnItemCommand="rptStudents_ItemCommand" 
          OnItemDataBound="rptStudents_ItemDataBound">
          <ItemTemplate>
            <!-- Student Data Row -->
            <tr>
              <td>
                <!-- Hidden field for student id -->
                <asp:HiddenField ID="hfStudentId" runat="server" Value='<%# Eval("id") %>' />
                <!-- Student name label -->
                <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("student_name") %>'></asp:Label>
              </td>
              <td><%# Eval("student_email") %></td>
              <td><%# Eval("student_mobile") %></td>
              <td>
                <asp:Button ID="btnExpand" runat="server" Text="+" 
                  CommandName="ExpandSubjects" 
                  CommandArgument='<%# Eval("id") %>' 
                  CssClass="expand-btn" />
              </td>
            </tr>
            <!-- Subjects Detail Row -->
            <tr style="display:<%# GetDisplayStyle(Eval("id")) %>;" class="subject-row">
              <td colspan="4">
                <div class="subject-container">
                  <asp:Repeater ID="rptSubjects" runat="server">
                    <ItemTemplate>
                      <div class="subject-item">
                        <span>
                          <%# Container.ItemIndex + 1 %>. <%# Eval("subject_name") %>
                        </span>
                        <!-- Hidden field for subject name (if needed for display) -->
                        <asp:HiddenField ID="hfSubjectName" runat="server" Value='<%# Eval("subject_name") %>' />
                        <!-- New hidden field for subject id -->
                        <asp:HiddenField ID="hfSubjectId" runat="server" Value='<%# Eval("subject_id") %>' />
                        <!-- TextBox for entering marks -->
                        <asp:TextBox ID="txtMarks" runat="server" CssClass="mark-textbox" EnableViewState="true" />
                      </div>
                    </ItemTemplate>
                  </asp:Repeater>
                </div>
              </td>
            </tr>
          </ItemTemplate>
        </asp:Repeater>
      </tbody>
    </table>
    
    <!-- Global Submit Button -->
    <div style="text-align:right;">
      <asp:Button ID="btnSubmitAll" runat="server" Text="Submit All Marks" 
        OnClick="btnSubmitAll_Click" CssClass="global-submit-button" />
    </div>
  </div>
</asp:Content>
