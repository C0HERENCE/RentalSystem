<template>
  <div style="width: 100%" class="my-3">
    <el-table :data="tableData">
      <el-table-column prop="id" label="编号"></el-table-column>
      <el-table-column label="账户状态">
        <template slot-scope="xxx">
          <el-tag type="success" v-if="xxx.row.enabled">正常</el-tag>
          <el-tag type="danger" v-else>封禁</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="schoolNum" label="学号"></el-table-column>
      <el-table-column prop="student.name" label="姓名"></el-table-column>
      <el-table-column prop="student.idCard" label="身份证"></el-table-column>
      <el-table-column prop="email" label="邮箱地址"></el-table-column>
      <el-table-column label="编辑">
        <template slot-scope="xxx">
          <el-button @click="openDialog(xxx.row)" size="small"><i class="el-icon-edit"></i></el-button>
        </template>
      </el-table-column>
    </el-table>

    <el-dialog title="学生详情" :visible.sync="dialogVisible" width="30%">
      <el-form>
        <el-form-item label="账户状态">
          <el-tag type="success" v-if="currentStudent.enabled">正常</el-tag>
          <el-tag type="danger" v-else>封禁</el-tag>
          <el-button class="mx-3" type="warning" @click="onChange(currentStudent)" size="mini">修改</el-button>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import {GetAllUsers, UpdateUserById} from "@/api/admin";


export default {
  name: "UserManagement",
  mounted() {
    GetAllUsers().then(res => this.tableData = res.data.data)
  },
  data() {
    return {
      tableData: [],
      dialogVisible: false,
      currentStudent: {}
    }
  },
  methods: {
    openDialog(row) {
      this.currentStudent = row;
      this.dialogVisible = true;
    },
    onChange(row) {
      let x = !row.enabled;
      UpdateUserById(row.id, x).then(res => {
        this.$message({
          message: res.data.message,
          type: res.data.status === 200 ? 'success' : 'danger'
        });
        if (res.data.status === 200)
          this.tableData.find(z => z.id === this.currentStudent.id).enabled = x;
      })
    },
  },
}
</script>

<style scoped>

</style>