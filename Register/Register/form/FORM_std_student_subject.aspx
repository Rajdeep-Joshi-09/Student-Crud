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
    .global-submit-button { background: linear-gradient(to bottom, #ff9800, #e68a00); color: #fff; border: none; border-radius: 6px; padding: 8px 16px; cursor: pointer; font-weight: bold; transition: background 0.3s ease; margin-top: 20px; margin-right: 10px; }
    .global-submit-button:hover { background: linear-gradient(to bottom, #e68a00, #ff9800); }
  </style>
  <!-- Include jQuery -->
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
  <!-- Hidden iframe for Excel export -->
  <iframe id="downloadFrame" style="display:none;"></iframe>
  <script type="text/javascript">
      var expandedStudents = [];
      var studentMarks = {}; // client‑side state

      $(document).ready(function () {
          // Load standards via AJAX.
          $.ajax({
              type: "POST",
              url: "FORM_std_student_subject.aspx/GetStandards",
              data: "{}",
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (response) {
                  var standards = JSON.parse(response.d);
                  var $stdList = $("#<%= stdList.ClientID %>");
                  $stdList.empty();
                  $stdList.append($("<option>", { value: "0", text: "-- Select Standard --" }));
                  $.each(standards, function (i, standard) {
                      $stdList.append($("<option>", { value: standard.std_id, text: standard.std_name }));
                  });
              },
              error: function (err) {
                  console.log(err);
              }
          });

          // When standard is changed, load students.
          $("#<%= stdList.ClientID %>").change(function () {
              var stdId = $(this).val();
              expandedStudents = [];
              studentMarks = {};
              if (stdId == "0") {
                  $("#lblMessage").text("Please select a valid standard.");
                  $("#studentsContainer").html("");
              } else {
                  $.ajax({
                      type: "POST",
                      url: "FORM_std_student_subject.aspx/GetStudentsByStandard",
                      data: JSON.stringify({ stdId: parseInt(stdId) }),
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (response) {
                          var students = JSON.parse(response.d);
                          var html = "";
                          $.each(students, function (i, student) {
                              html += "<tr id='student-row-" + student.id + "'>";
                              html += "<td>" + student.student_name + "</td>";
                              html += "<td>" + student.student_email + "</td>";
                              html += "<td>" + student.student_mobile + "</td>";
                              html += "<td><button type='button' class='expand-btn' data-studentid='" + student.id + "'>+</button></td>";
                              html += "</tr>";
                              html += "<tr class='subject-row' id='subject-row-" + student.id + "' style='display:none;'>";
                              html += "<td colspan='4'><div class='subject-container' id='subject-container-" + student.id + "'></div></td>";
                              html += "</tr>";
                          });
                          $("#studentsContainer").html(html);
                          $("#lblMessage").text("");
                      },
                      error: function (err) {
                          console.log(err);
                      }
                  });
              }
          });

          // Expand/collapse subjects.
          $(document).on("click", ".expand-btn", function () {
              var studentId = $(this).data("studentid");
              var stdId = $("#<%= stdList.ClientID %>").val();
              var $subjectRow = $("#subject-row-" + studentId);
              if ($subjectRow.is(":visible")) {
                  $subjectRow.hide();
                  var idx = expandedStudents.indexOf(studentId);
                  if (idx > -1) expandedStudents.splice(idx, 1);
              } else {
                  $.ajax({
                      type: "POST",
                      url: "FORM_std_student_subject.aspx/GetSubjectsByStudent",
                      data: JSON.stringify({ studentId: parseInt(studentId), stdId: parseInt(stdId) }),
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (response) {
                          var subjects = JSON.parse(response.d);
                          var html = "";
                          $.each(subjects, function (i, subject) {
                              html += "<div class='subject-item'>";
                              html += "<span>" + (i + 1) + ". " + subject.subject_name + "</span>";
                              html += "<input type='text' class='mark-textbox' data-studentid='" + studentId + "' data-subjectid='" + subject.subject_id + "' />";
                              html += "</div>";
                          });
                          $("#subject-container-" + studentId).html(html);
                          $subjectRow.show();
                          if (expandedStudents.indexOf(studentId) === -1)
                              expandedStudents.push(studentId);
                      },
                      error: function (err) {
                          console.log(err);
                      }
                  });
              }
          });

          // Save mark on change.
          $(document).on("change", ".mark-textbox", function () {
              var studentId = $(this).data("studentid");
              var subjectId = $(this).data("subjectid");
              var mark = $(this).val().trim();
              if (!studentMarks[studentId])
                  studentMarks[studentId] = {};
              studentMarks[studentId][subjectId] = mark;
          });

          // Submit all marks (preventing full postback).
          $("#<%= btnSubmitAll.ClientID %>").click(function () {
              // Check if a valid standard is selected.
              var stdId = $("#<%= stdList.ClientID %>").val();
              if (stdId == "0") {
                  alert("Please select a valid standard.");
                  return false;
              }

              var missingMessages = [];
              $.each(expandedStudents, function (i, studentId) {
                  $("#subject-container-" + studentId + " .mark-textbox").each(function () {
                      var subjectId = $(this).data("subjectid");
                      var mark = $(this).val().trim();
                      if (mark === "") {
                          var studentName = $("#student-row-" + studentId + " td:first").text();
                          var subjectName = $(this).closest(".subject-item").find("span").text();
                          missingMessages.push("The marks for " + subjectName + " for " + studentName + " are missing.");
                      }
                  });
              });
              if (missingMessages.length > 0) {
                  alert(missingMessages.join("\n"));
                  return false;
              }
              var entries = [];
              $(".mark-textbox").each(function () {
                  var val = $(this).val().trim();
                  if (val !== "") {
                      entries.push({
                          StudentId: $(this).data("studentid"),
                          SubjectId: $(this).data("subjectid"),
                          Mark: parseFloat(val)
                      });
                  }
              });
              $.ajax({
                  type: "POST",
                  url: "FORM_std_student_subject.aspx/InsertStudentMarks",
                  data: JSON.stringify({ entries: entries }),
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (response) {
                      alert("Marks are inserted.");
                  },
                  error: function (err) {
                      console.log(err);
                  }
              });
              return false;
          });

          // Excel Export: trigger via hidden iframe.
          $("#<%= btnExportToExcel.ClientID %>").click(function () {
              var stdId = $("#<%= stdList.ClientID %>").val();
              if (stdId == "0") {
                  alert("Please select a valid standard.");
                  return false;
              }
              // Set iframe src to trigger export.
              $("#downloadFrame").attr("src", "FORM_std_student_subject.aspx?export=1&stdId=" + stdId);
              return false;
          });

          // Excel Import: postback will occur normally.
          $("#<%= btnImportExcel.ClientID %>").click(function () {
              // Let the server handle Excel import via postback.
              return true;
          });
      });
  </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
  
  <div class="outer-container">
    <!-- Standard Dropdown -->
    <div style="margin-bottom:20px;">
      <label style="font-weight:bold;">Select Standard:</label>
      <asp:DropDownList ID="stdList" runat="server" CssClass="std-dropdown"></asp:DropDownList>
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
      <tbody id="studentsContainer">
        <!-- Loaded via JavaScript -->
      </tbody>
    </table>
    
    <!-- Global Buttons: Submit, Export, Import -->
    <div style="text-align:right;">
      <asp:Button ID="btnSubmitAll" runat="server" Text="Submit All Marks" CssClass="global-submit-button" OnClientClick="return false;" />
      <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" CssClass="global-submit-button" OnClientClick="return false;" />
      <asp:FileUpload ID="fuExcelImport" runat="server" />
      <asp:Button 
    ID="btnImportExcel" 
    runat="server" 
    Text="Import Excel Marks" 
    CssClass="global-submit-button" 
    OnClick="btnImportExcel_Click" />

    </div>
  </div>
</asp:Content>
