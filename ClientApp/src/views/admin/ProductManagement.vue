<template>
  <div style="width: 100%" class="my-3">
<!--    {{ tableData[0] }}-->
    <el-table :data="tableData">
      <el-table-column prop="id" label="编号"></el-table-column>
      <el-table-column prop="name" label="商品名称"></el-table-column>
      <el-table-column prop="checked" label="审核状态">
        <template slot-scope="{row}">
          <el-tag type="success" v-if="row.checked === 1">已通过</el-tag>
          <el-tag type="danger" v-else-if="row.checked === 2">未通过</el-tag>
          <el-tag type="warning" v-else>待审核</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="enabled" label="在售状态">
        <template slot-scope="{row}">
          <el-tag type="success" v-if="row.enabled">在售</el-tag>
          <el-tag type="danger" v-else>已下架</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="keywords" label="关键词标签">
        <template slot-scope="{row}" v-if="row.keywords !== null">
          <el-tag size="mini" v-for="tag in row.keywords.split(',')" :key="tag" v-show="tag.length > 0">
            {{ tag }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="publisher.schoolNum" label="发布者学号"></el-table-column>
      <el-table-column prop="publisher.student.name" label="发布者"></el-table-column>

      <el-table-column label="详情编辑">
        <template slot-scope="x">
          <el-button @click="current = x.row" size="small"><i class="el-icon-edit"></i></el-button>
        </template>
      </el-table-column>
    </el-table>


    <!--    编辑框部分-->
    <el-dialog title="商品详情" :visible="current !== null" width="30%" v-if="current" @close="current = null">
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
        <el-form-item label="在售状态">
          <el-tag type="success" v-if="current.enabled">在售</el-tag>
          <el-tag type="danger" v-else>已下架</el-tag>
          <div class="text-right">
            <el-button v-if="current.enabled !== 1" @click="onChangeEnable(1)" size="mini" type="success">上架
            </el-button>
            <el-button v-else @click="onChangeEnable(0)" size="mini" type="danger">下架</el-button>
          </div>
        </el-form-item>

        <el-form-item label="标签&关键词">
          <el-tag :key="tag" v-for="tag in current.keywords.split(',')" closable :disable-transitions="false"
                  size="mini" v-show="tag.length > 0"
                  @close="onChangeTag(tag)"> {{ tag }}
          </el-tag>
          <el-input class="input-new-tag" v-if="tagVisible" v-model="tagInput" placeholder="输入"
                    ref="saveTagInput" size="small" @keyup.enter.native="handleInputConfirm">
          </el-input>
          <br>
          <el-button class="button-new-tag" size="mini" @click="() => {
            this.tagVisible = true;
             this.$nextTick(_ => {
                this.$refs.saveTagInput.$refs.input.focus();
             })
          }">+ 新标签
          </el-button>
        </el-form-item>
      </el-form>
    </el-dialog>
  </div>
</template>

<script>
import {GetProducts, UpdateProductById} from "@/api/admin";

export default {
  name: "ProductManagement",
  mounted() {
    GetProducts().then(res => this.tableData = res.data.data)
  },
  data() {
    return {
      tableData: [],
      current: null,
      tagInput: '',
      tagVisible: false
    }
  },
  methods: {
    // 改变审核状态
    onChangeChecked(checked) {
      if (checked !== 1 && this.current.enabled === 1) {
        this.$alert("请先下架")
        return
      }
      UpdateProductById(this.current.id, checked, this.current.enabled, this.current.categoryId, this.current.keywords).then(res => {
        if (res.data.status === 200) {
          this.current.checked = checked
          this.$message.success(res.data.message)
        } else {
          this.$message.error(res.data.message)
        }
      })
    },
    // 改变上架状态
    onChangeEnable(enabled) {
      console.log(this.current)
      if (this.current.checked !== 1 && enabled === 1) {
        this.$alert("请先通过审核")
        return;
      }
      UpdateProductById(this.current.id, this.current.checked, enabled, this.current.categoryId, this.current.keywords).then(res => {
        if (res.data.status === 200) {
          this.current.enabled = enabled
          this.$message.success(res.data.message)
        } else {
          this.$message.error(res.data.message)
        }
      })
    },
    // 删除关键词
    onChangeTag(tag) {
      let keywords = this.current.keywords.split(',')
      let newKeyword = ""
      for (let keyword of keywords) {
        if (keyword === tag)
          continue;
        newKeyword += keyword + ','
      }
      if (newKeyword.length > 0)
        newKeyword = newKeyword.substr(0, newKeyword.length - 1)
      UpdateProductById(this.current.id, this.current.checked, this.current.status, this.current.categoryId, newKeyword).then(res => {
        if (res.status === 200) {
          this.$message.success(res.data.message)
          this.current.keywords = newKeyword;
        } else {
          this.$message.error(res.data.message)
        }
      })
    },
    // 添加关键词
    handleInputConfirm() {
      if (this.tagInput.trim() === "" || this.tagInput.length > 10) {
        this.$message.error("关键词长度必须在1~10之间")
        return
      }
      if (this.current.keywords.split(',').length >= 6)
        this.$message.error("最多添加6个标签")

      let newKeyword = this.current.keywords;
      if (newKeyword.length > 0)
        newKeyword = this.current.keywords + ',' + this.tagInput
      else
        newKeyword = this.tagInput
      UpdateProductById(this.current.id, this.current.checked, this.current.status, this.current.categoryId, newKeyword).then(res => {
        if (res.status === 200) {
          this.$message.success(res.data.message)
          this.current.keywords = newKeyword;
          this.tagInput = "";
          this.tagVisible = false;
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