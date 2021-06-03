<template>
  <el-dialog v-if="$store.state.messages[userId]" :visible="visible" @close="visible = false"
             :title="$store.state.messages[userId].name">
    <div style="height: 350px;overflow-y: scroll">
      <div v-for="i in $store.state.messages[userId].data" :key="i.id">

        <span v-if="i.dir === 0" class="font-weight-bold">{{ $store.state.messages[userId].name }} 说： ( {{ i.time | fTime }})</span>
        <span v-else class="font-weight-bold">我说： ( {{ i.time | fTime }})</span>
        <p>{{ i.message }}</p>
        <hr>
      </div>
    </div>
    <template slot="footer">
      <el-row :gutter="24">
        <el-col :span="20">
          <el-input type="text" v-model="message"></el-input>
        </el-col>
        <el-col :span="4">
          <el-button style="width: 100%" @click="SendMessage">发送</el-button>
        </el-col>
      </el-row>
    </template>
  </el-dialog>
</template>

<script>
export default {
  name: "ChatWindow",
  data() {
    return {
      visible: false,
      userId: '',
      message: ''
    }
  },
  methods: {
    SendMessage() {
      this.$emit('sendMessage', this.userId, this.message)
      this.message = ""
    }
  },
}
</script>

<style scoped>


</style>