<template>
  <div style="width: 100%" class="my-3">
    <!--    {{ tableData[0] }}-->
    <el-table :data="tableData">
      <el-table-column prop="id" label="编号"></el-table-column>
      <el-table-column prop="checked" label="审核状态">
        <template slot-scope="{row}">
          <el-tag type="success" v-if="row.checked === 1">已通过</el-tag>
          <el-tag type="danger" v-else-if="row.checked === 2">未通过</el-tag>
          <el-tag type="warning" v-else>待审核</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="enabled" label="展示状态">
        <template slot-scope="{row}">
          <el-tag type="success" v-if="row.enabled">展示中</el-tag>
          <el-tag type="danger" v-else>未展示</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="name" label="需求名称"></el-table-column>
      <el-table-column prop="publisher.schoolNum" label="发布者学号"></el-table-column>
      <el-table-column prop="publisher.student.name" label="发布者"></el-table-column>


      <el-table-column label="详情编辑">
        <template slot-scope="x">
          <el-button @click="current = x.row" size="small"><i class="el-icon-edit"></i></el-button>
        </template>
      </el-table-column>
    </el-table>


    <!--    编辑框部分 -->
    <el-dialog title="需求详情" :visible="current !== null" width="30%" v-if="current" @close="current = null">
      <el-form @submit.native.prevent>
        <!--        当一个 form 元素中只有一个输入框时，在该输入框中按下回车应提交该表单。如果希望阻止这一默认行为，可以在 <el-form> 标签上添加 @submit.native.prevent。-->
        <el-form-item label="审核状态">
          <el-tag type="success" v-if="current.checked === 1">已通过</el-tag>
          <el-tag type="danger" v-else-if="current.checked === 2">未通过</el-tag>
          <el-tag type="warning" v-else>待审核</el-tag>
          <div class="text-right">
            <el-button v-if="current.checked !== 1" @click="onChangeChecked(1)" size="mini" type="success">
              通过审核
            </el-button>
            <el-button v-if="current.checked !== 2" @click="onChangeChecked(2)" size="mini" type="danger">
              未通过审核
            </el-button>
            <el-button v-if="current.checked !== 0" @click="onChangeChecked(0)" size="mini" type="warning">
              设为待审核
            </el-button>
          </div>
        </el-form-item>
        <el-form-item label="展示状态">
          <el-tag type="success" v-if="current.enabled === true">展示中</el-tag>
          <el-tag type="danger" v-else>未展示</el-tag>
          <div class="text-right">
            <el-button v-if="current.enabled !== true" @click="onChangeEnable(true)" size="mini" type="success">上架
            </el-button>
            <el-button v-else @click="onChangeEnable(false)" size="mini" type="danger">下架</el-button>
          </div>
        </el-form-item>
      </el-form>
    </el-dialog>

  </div>
</template>


<script>
import {GetDemands, UpdateDemandById} from "@/api/admin";

export default {
  name: "DemandManagement",
  mounted() {
    GetDemands().then(res => this.tableData = res.data.data)
  },
  data() {
    return {
      tableData: [],
      current: null,
    }
  },
  methods: {
    onChangeChecked(checked) {
      if (checked !== 1 && this.current.enabled === true) {
        this.$alert("请先下架")
        return
      }
      UpdateDemandById(this.current.id, checked, this.current.enabled).then(res => {
        if (res.data.status === 200) {
          this.current.checked = checked
          this.$message.success(res.data.message)
        } else {
          this.$message.error(res.data.message)
        }
      })
    },
    onChangeEnable(enabled) {
      if (this.current.checked !== 1 && enabled === true) {
        this.$alert("请先通过审核")
        return;
      }
      UpdateDemandById(this.current.id, this.current.checked, enabled).then(res => {
        if (res.data.status === 200) {
          this.current.enabled = enabled
          this.$message.success(res.data.message)
        } else {
          this.$message.error(res.data.message)
        }
      })
    }
  },
}
</script>

<style scoped>

</style>