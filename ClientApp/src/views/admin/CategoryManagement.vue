<template>
    <div style="width:70%; margin: 0 auto" class="my-3">
      <el-button @click="openAdd">新增分类</el-button>
      <b-list-group>
        <b-list-group-item v-for="c in categoryData" :key="c.id">
          {{c.name}}
          <el-button size="mini" class="float-right" @click="open(c)">
            <i class="el-icon-edit">重命名</i>
          </el-button>
          <el-button size="mini" class="float-right" v-if="c.enabled" @click="changeEnabled(c)">关闭分类</el-button>
          <el-button size="mini" class="float-right" v-else @click="changeEnabled(c)">开启分类</el-button>
        </b-list-group-item>
      </b-list-group>
    </div>
</template>

<script>
import {GetCategories, UpdateCategory, AddCategories} from "@/api/admin";

export default {
  name: "CategoryManagement",
  mounted() {
    this.categoryData = GetCategories().then(res => {
      this.categoryData = res.data.data;
    })
  },
  data() {
    return {
      categoryData: [],
    }
  },
  methods: {
    open(c) {
      this.$prompt('请输入新名称', '重命名分类名', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
      }).then(({value}) => {
       if (value.trim() === "") {
         this.$alert("名称不能为空");
         return;
       }
       UpdateCategory(c.id, value, c.enabled).then(res => {
         if (res.data.status === 200) {
           this.$message.success(res.data.message);
           c.name = value
         } else {
           this.$message.error(res.data.message);
         }
       })
      })
    },
    openAdd() {
      this.$prompt('请输入名称', '添加分类名', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
      }).then(({value}) => {
        if (value.trim() === "") {
          this.$alert("分类名称不能为空");
          return;
        }
        AddCategories(value).then(res => {
          if (res.data.status === 200) {
            this.$message.success(res.data.message);
          } else {
            this.$message.error(res.data.message);
          }
        })
      })
    },
    changeEnabled(c){
      UpdateCategory(c.id, c.name, !c.enabled).then(res => {
        if (res.data.status === 200) {
          this.$message.success(res.data.message)
          c.enabled = !c.enabled
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