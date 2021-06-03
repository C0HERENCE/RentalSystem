<template>
  <div style="width: 100%" class="my-3">
    <el-table :data="tableData">
      <el-table-column prop="id" label="编号"></el-table-column>
      <el-table-column prop="name" label="姓名"></el-table-column>
      <el-table-column prop="schoolNum" label="学号"></el-table-column>
      <el-table-column prop="idCard" label="身份证号"></el-table-column>
<!--      <el-table-column prop="class.id" label="班级编号"></el-table-column>-->
      <el-table-column label="班级">
        <template slot-scope="{row}">
          <span>{{ row.class.grade }}</span>
          <span>{{ row.class.major }}</span>
        </template>
      </el-table-column>
      <el-table-column label="信息编辑">
        <template slot-scope="{row}">
          <el-button @click="onFormOpen(row)" size="small"><i class="el-icon-edit"></i></el-button>
        </template>
      </el-table-column>
    </el-table>


    <el-dialog title="学生详情" :visible="current !== null" width="30%" v-if="current" @close="current=null">
      <el-form v-model="studentFormData">
        <el-form-item label="姓名">
          <el-input v-model="studentFormData.name"></el-input>
        </el-form-item>
        <el-form-item label="学号">
          <el-input v-model="studentFormData.schoolNum"></el-input>
        </el-form-item>
        <el-form-item label="身份证号">
          <el-input v-model="studentFormData.idCard"></el-input>
        </el-form-item>
        <el-form-item label="班级">
          <el-select v-model="studentFormData.classId">
            <el-option v-for="i in classData" :key="i.id" :value="i.id" :label="i.grade + ' ' + i.major"></el-option>
          </el-select>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="current=null">取 消</el-button>
        <el-button type="primary" @click="onFormClosing">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import {GetAllStudents, GetClasses, UpdateStudentById} from "@/api/admin";

export default {
  name: "StudentManagement",
  mounted() {
    GetAllStudents().then(res => {
      this.tableData = res.data.data;
    })
    GetClasses().then(res => {
      this.classData = res.data.data;
    })
  },
  data() {
    return {
      tableData: [],
      current: null,
      classData: [],
      studentFormData: {}
    }
  },
  methods: {
    onFormClosing() {
      UpdateStudentById(
          this.current.id,
          this.studentFormData.name,
          this.studentFormData.schoolNum,
          this.studentFormData.idCard,
          this.studentFormData.classId).then(res => {
            if (res.data.status === 200) {
              this.$message.success(res.data.message);
              this.current.name = this.studentFormData.name;
              this.current.schoolNum = this.studentFormData.schoolNum;
              this.current.idCard = this.studentFormData.idCard;
              this.current.classId = this.studentFormData.classId;
              this.studentFormData = {};
            } else {
              this.$message.error(res.data.message);
            }
      })
    },
    onFormOpen(row) {
      this.current=row
      this.studentFormData = {
        name: row.name,
        idCard: row.idCard,
        schoolNum: row.schoolNum,
        classId: row.classId,
      }
    }
  },
}
</script>

<style scoped>

</style>