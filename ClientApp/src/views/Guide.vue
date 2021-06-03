<template>
  <div>
    <h3 style="text-align: center;margin: 30px 0">租赁指南</h3>
    <el-collapse v-model="activeName" accordion>
      <el-collapse-item title="租赁期如何计算" name="1">
        <div>租赁期计算方式为现场交货当天起，至现场收货验货止。 我们会按照您要求的协商送达物品。</div>
        <div>例如： 您要求4月10号收到，租赁期为5天，那么偿还时间为4月15日。</div>
        <div>重要提示：为了确保押金的全额偿还，请在规定时间内进行租赁商品的返还。</div>
      </el-collapse-item>
      <el-collapse-item title="订单是否可以续租" name="2">
        <div>很抱歉，本系统订单不支持续租，交易结束之后自动关闭订单。如需续租，请与发布者联系，并再次下单。</div>
      </el-collapse-item>
      <el-collapse-item title="关于订单的提前归还" name="3">
        <div>很抱歉，由于产品档期安排原因，当您提前归还时，剩余租金不会退还。请在下单前确认下单的天数，谢谢配合！</div>
      </el-collapse-item>
      <el-collapse-item title="押金收取及退还" name="4">
        <div>押金收取由商品的发布者规定，如有异议可与发布者联系，请确认押金金额之后再下单。</div>
        <div>租赁期截止前，将商品当面返还给发布者，进行现场验收，若商品有损，双方协商后由发布者在押金中扣除一部分作为补偿，若商品完好无损，则全额退还押金。</div>
      </el-collapse-item>
      <el-collapse-item title="隐私协议" name="5">
        <div>请仔细阅读以下条款，当您注册成功，意味着您完全接受本协议。</div>
        <div>
          我们将采取合理的措施尽最大可能保护您的个人资料。每当您提供给我们信息时，我们将采取合理的措施保护您的个人资料，我们也将采取合理的安全手段保护已存储的个人资料。除非根据法律或政策的强制性规定，在未得到您的许可之前，我们不会把您的任何个人资料提供给无关的第三方。
        </div>
        <div>隐私原则： 您一旦注册成功，将得到一个密码和帐号。如果您不保管好自己的帐号和密码安全，将负全部责任。另外，每个用户都要对其帐户中的所有活动和事件负全责。您可随时根据指示改变您的密码以及个人资料。</div>
      </el-collapse-item>
    </el-collapse>
    <div class="alert alert-info" role="alert" style="margin: 20px 0">请务必仔细阅读以上指南，否则后果自负。</div>

  </div>
</template>

<script>
export default {
  name: "Guide",
  data() {
    return {
      activeName: '1'
    };
  }
}
</script>

<style scoped>

</style>