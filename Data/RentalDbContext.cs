using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RentalServer.Model;

namespace RentalServer.Data
{
    // 继承
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options) // 委托构造
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Money> Money { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<FavouriteRecord> FavouriteRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<FavouriteRecord>()
            modelBuilder.Entity<User>()
                .HasData(new List<User>()
                {
                    new User()
                    {
                        Id = 1, Balance = 10000, Description = "这是我的个人介绍。", Email = "749976734@qq.com", Enabled = true,
                        IsAdmin = false, Password = "C06C0E3DE692C9888AD5DE7836B7321E3CBC6E272AA638E1E01C4622027221F0",
                        SchoolNum = "1706010018", StudentId = 1
                    },
                    new User()
                    {
                        Id = 2, Balance = 10000, Description = "这是我的个人介绍。", Email = "2280484235@qq.com", Enabled = true,
                        IsAdmin = false, Password = "C06C0E3DE692C9888AD5DE7836B7321E3CBC6E272AA638E1E01C4622027221F0",
                        SchoolNum = "1706010007", StudentId = 2
                    },
                    new User()
                    {
                        Id = 3, Balance = 10000, Description = "这是我的个人介绍。", Email = "1152952072@qq.com", Enabled = true,
                        IsAdmin = false, Password = "C06C0E3DE692C9888AD5DE7836B7321E3CBC6E272AA638E1E01C4622027221F0",
                        SchoolNum = "1706010005", StudentId = 3
                    },
                    new User()
                    {
                        Id = 4, Balance = 10000, Description = "这是我的个人介绍。", Email = "846420752@qq.com", Enabled = true,
                        IsAdmin = true, Password = "C06C0E3DE692C9888AD5DE7836B7321E3CBC6E272AA638E1E01C4622027221F0",
                        SchoolNum = "1706010006", StudentId = 4
                    },
                });
            modelBuilder.Entity<Student>()
                .HasData(new List<Student>
                {
                    new Student
                    {
                        Id = 1,
                        ClassId = 1,
                        IdCard = "33042419980918000X",
                        Name = "杨宝宝",
                        SchoolNum = "1706010018"
                    },
                    new Student
                    {
                        Id = 2,
                        ClassId = 2,
                        IdCard = "330682199709160001",
                        Name = "谢盼",
                        SchoolNum = "1706010007"
                    },
                    new Student
                    {
                        Id = 3,
                        ClassId = 3,
                        IdCard = "330324199907030002",
                        Name = "黄小展",
                        SchoolNum = "1706010005"
                    },
                    new Student
                    {
                        Id = 4,
                        ClassId = 4,
                        IdCard = "330682199902070001",
                        Name = "邵真真",
                        SchoolNum = "1706010006"
                    },
                    new Student
                    {
                        Id = 5,
                        ClassId = 1,
                        IdCard = "330682199903240001",
                        Name = "任恬恬",
                        SchoolNum = "1706010010"
                    },
                });
            modelBuilder.Entity<Class>()
                .HasData(new List<Class>
                {
                    new Class
                    {
                        Id = 1,
                        Grade = "2017",
                        Major = "信息管理与信息系统"
                    },
                    new Class
                    {
                        Id = 2,
                        Grade = "2018",
                        Major = "信息管理与信息系统"
                    },
                    new Class
                    {
                        Id = 3,
                        Grade = "2019",
                        Major = "信息管理与信息系统"
                    },
                    new Class
                    {
                        Id = 4,
                        Grade = "2017",
                        Major = "生物医学工程"
                    },
                    new Class
                    {
                        Id = 5,
                        Grade = "2018",
                        Major = "生物医学工程"
                    },
                    new Class
                    {
                        Id = 6,
                        Grade = "2019",
                        Major = "生物医学工程"
                    },
                });
            modelBuilder.Entity<Category>()
                .HasData(new List<Category>
                {
                    new()
                    {
                        Id = 1,
                        Name = "数码产品",
                        Enabled = true
                    },
                    new()
                    {
                        Id = 2,
                        Name = "服装",
                        Enabled = true
                    },
                    new()
                    {
                        Id = 3,
                        Name = "图书",
                        Enabled = true
                    },
                    new()
                    {
                        Id = 4,
                        Name = "交通工具",
                        Enabled = true
                    },
                    new()
                    {
                        Id = 5,
                        Name = "鞋类箱包",
                        Enabled = true
                    },
                });
            modelBuilder.Entity<Demand>()
                .HasData(new List<Demand>
                {
                    new()
                    {
                        Id = 1,
                        Checked = 1,
                        Enabled = true,
                        Name = "自行车",
                        Content = "我想要租一辆小巧的自行车。",
                        PublisherId = 1,
                    },
                    new()
                    {
                        Id = 2,
                        Checked = 1,
                        Enabled = true,
                        Name = "演出服",
                        Content = "我想要租一件长款礼服，款式适合节目主持。",
                        PublisherId = 2,
                    },
                    new()
                    {
                        Id = 3,
                        Checked = 1,
                        Enabled = true,
                        Name = "照相机",
                        Content = "我想要租一台照相机，最好是佳能的。",
                        PublisherId = 2,
                    },
                    new()
                    {
                        Id = 4,
                        Checked = 1,
                        Enabled = true,
                        Name = "电脑",
                        Content = "想要租Windows系统的电脑一台。",
                        PublisherId = 4,
                    },
                    new()
                    {
                        Id = 5,
                        Checked = 1,
                        Enabled = true,
                        Name = "汉服",
                        Content = "有汉服吗？适合拍照的。",
                        PublisherId = 3,
                    },
                    new()
                    {
                        Id = 6,
                        Checked = 1,
                        Enabled = true,
                        Name = "线性代数",
                        Content = "想租一本线性代数教材，急！",
                        PublisherId = 2,
                    },
                });
            modelBuilder.Entity<Product>()
                .HasData(new List<Product>
                {
                    new()
                    {
                        Id = 1,
                        Checked = 1,
                        Enabled = 1,
                        Name = "图解数据结构",
                        Description = "图解数据结构 使用C# 数据结构算法零基础入门书 递归动态规划迭代 程序设计语言编写 计算机网络书籍",
                        Details =
                            "这是一本综合讲述数据结构及其算法的入门书，全书采用图文讲解的方式，力求读者易于学习和掌握。 全书从基本的数据结构概念开始讲起，包括数组结构、队列、堆栈、树形结构、排序、查找等；接着介绍常用的算法，包括分治法、递归法、贪心法、动态规划法、迭代法、枚举法、回溯法等，并为每个经典的算法都提供了C#程序设计语言编写的完整范例程序；*后在每章末尾都安排了大量的习题，这些题目包含各类考试的例题，希望读者能灵活地应用所学的各种知识。 本书图文并茂，叙述简洁、清晰，范例丰富，可操作性强，针对具有一定编程能力又想提高编程“深度”的非信息专业类人员或学生，是一本数据结构普及型的教科书或自学参考书。",
                        PublisherId = 1,
                        Price = 3,
                        CategoryId = 3,
                        Keywords = "学习,计算机",
                        YaMoney = 20,
                        Cover = "图解数据结构.jpg",
                    },
                    new()
                    {
                        Id = 2,
                        Checked = 1,
                        Enabled = 1,
                        Name = "计算机导论",
                        Description = "科学出版社编 郑逢斌著科学出版社编 郑逢斌著",
                        Details =
                            "《计算机导论》是学习计算机专业知识的入门课程教材，是计算机专业完整知识体系的绪论。本书在各个章节的安排中，根据理论知识的阐述，尽量列举出计算机每个知识领域在实际生活中的简单应用，并在内容上做到承上启下、系统性的编排。全书分为两部分：第一部分共11章；第二部分为实验的具体内容。本书由房彩丽、罗慧敏、王强、苗茹和赵雅靓、潘伟合作完成。全书由郑逢斌教授统一审稿和定稿。",
                        PublisherId = 3,
                        Price = 5,
                        CategoryId = 3,
                        Keywords = "学习,计算机",
                        YaMoney = 25,
                        Cover = "计算机导论.jpg",
                    },
                    new()
                    {
                        Id = 3,
                        Checked = 1,
                        Enabled = 1,
                        Name = "算法图解",
                        Description = "人民邮电出版社编 (美)巴尔加瓦著",
                        Details =
                            "由美国巴尔加瓦所著、袁国忠翻译的《算法图解(像小说一样有趣的算法入门书)/图灵程序设计丛书》一书示例丰富，图文并茂，以简明易懂的方式阐释了算法，旨在帮助程序员在日常项目中更好地利用算法为软件开发助力。前三章介绍算法基础，包括二分查找、大O表示法、两种基本的数据结构以及递归等。余下的篇幅将主要介绍应用广泛的算法，具体内容包括：面对具体问题时的解决技巧，比如何时采用贪婪算法或动态规划；散列表的应用；图算法；K最近邻算法。本书适合所有程序员、计算机专业相关师生以及对算法感兴趣的读者。",
                        PublisherId = 3,
                        Price = 5,
                        CategoryId = 3,
                        Keywords = "学习,计算机",
                        YaMoney = 26,
                        Cover = "算法图解.jpg",
                    },
                    new()
                    {
                        Id = 4,
                        Checked = 1,
                        Enabled = 1,
                        Name = "Web应用开发技术",
                        Description = "清华大学出版社编 石双元著",
                        Details =
                            "  本书力图系统、全面地介绍Web应用开发所涉及的内容和最新进展，围绕Web应用开发所涉及的技术由浅入深地展开。在内容和结构安排上力求做到系统性和连贯性。首先讲述了面向对象技术的基本概念和特性；接着简要介绍了C#语言的基础知识以及面向对象特性在C#语言中的表现形式和实现方法，为了避免重复，将C#的类的设计和高级编程技术等内容融合到后面的代码分离技术和自定义控件的设计中；然后对ASP.NET开发涉及的背景知识进行了简单的介绍，以使没有相关背景知识的读者也能很快熟悉和掌握这些知识。在此基础上完整介绍了Microsoft ASP.NET的框架、服务器控件和基于ADO.NET的数据库开发技术。作为高级应用部分，本书还介绍了作为客户端开发的主流技术JavaScript及其对象，融入最新Ajax技术的原理和常用的框架与控件，如Microsoft ASP.NET Ajax。",
                        PublisherId = 1,
                        Price = 4,
                        CategoryId = 3,
                        Keywords = "学习,计算机,前端",
                        YaMoney = 17,
                        Cover = "Web应用开发技术.jpg",
                    },
                    new()
                    {
                        Id = 5,
                        Checked = 1,
                        Enabled = 1,
                        Name = "计算机安全(原理与实践)",
                        Description = "机械工业出版社编 (美)威廉·斯托林斯//(澳)劳里·布朗著",
                        Details =
                            "威廉·斯托林斯、劳里·布朗著的《计算机安全(原理与实践原书第4版)/计算机科学丛书》是计算机安全领域的经典教材，系统介绍了计算机安全的方方面面。全书包括5个部分：第一部分介绍计算机安全技术和原理，涵盖了支持有效安全策略所必需的所有技术领域；第二部分介绍软件和系统安全，主要涉及软件开发和运行带来的安全问题及相应的对策；第三部分介绍管理问题，主要讨论信息安全与计算机安全在管理方面的问题，以及与计算机安全相关的法律与道德方面的问题；第四部分为密码编码算法，包括各种类型的加密算法和其他类型的加密算法；第五部分介绍网络安全，关注的是为Internet上的通信提供安全保障的协议和标准及无线网络安全等问题。 本书覆盖面广，叙述清晰，可作为高等院校计算机安全课程的教材，同时也是一本关于密码学和计算机网络安全方面的非常有价值的参考书。",
                        PublisherId = 2,
                        Price = 6,
                        CategoryId = 3,
                        Keywords = "学习,计算机",
                        YaMoney = 27,
                        Cover = "计算机安全.jpg",
                    },
                    new()
                    {
                        Id = 6,
                        Checked = 1,
                        Enabled = 1,
                        Name = "算法导论(原书第3版)",
                        Description = "机械工业出版社编 (美)托马斯·科尔曼//查尔斯·雷瑟尔森//罗纳德·李维斯特//克利福德·斯坦著",
                        Details =
                            "  Thomas H. Cormen、Charles E. Leiserson、Ronald L. Rivest、Clifford Stein编著的《算法导论》提供了对当代计算机算法研究的一个全面、综合性的介绍。全书共八部分，内容涵盖基础知识、排序和顺序统计量、数据结构、高级设计和分析技术、高级数据结构、图算法、算法问题选编，以及数学基础知识。书中深入浅出地介绍了大量的算法及相关的数据结构，以及用于解决一些复杂计算问题的高级策略(如动态规划、贪心算法、摊还分析等)，重点在于算法的分析与设计。对于每一个专题，作者都试图提供目前最新的研究成果及样例解答，并通过清晰的图示来说明算法的执行过程。此外，全书包含957道练习和158道思考题，并且作者在网站上给出了部分题的答案。《算法导论》内容丰富，叙述深入浅出，适合作为计算机及相关专业本科生数据结构课程和研究生算法课程的教材，同时也适合专业技术人员参考使用。",
                        PublisherId = 2,
                        Price = 5,
                        CategoryId = 3,
                        Keywords = "学习,计算机",
                        YaMoney = 30,
                        Cover = "算法导论.jpg",
                    },
                    new()
                    {
                        Id = 7,
                        Checked = 1,
                        Enabled = 1,
                        Name = "汉服",
                        Description = "【行香子】褚云令汉服宋制霞帔对交穿日常清新五件套",
                        Details = "面料: 氨纶，尺码: M，建议身高：160-165cm。",
                        PublisherId = 1,
                        Price = 20,
                        CategoryId = 2,
                        Keywords = "古风",
                        YaMoney = 100,
                        Cover = "汉服.jpg",
                    },
                    new()
                    {
                        Id = 8,
                        Checked = 1,
                        Enabled = 1,
                        Name = "红军演出服",
                        Description = "女款",
                        Details = "材质: 棉，尺码: M，建议身高：160-165cm。",
                        PublisherId = 2,
                        Price = 16,
                        CategoryId = 2,
                        Keywords = "演出,女",
                        YaMoney = 86,
                        Cover = "红军演出服.jpg",
                    },
                    new()
                    {
                        Id = 9,
                        Checked = 1,
                        Enabled = 1,
                        Name = "主持人长裙",
                        Description = "礼服裙星空主持人长裙",
                        Details = "品牌: 衣礼服人，适用年龄: 18-25周岁，尺码:  M ，韩版腰型: 中腰。",
                        PublisherId = 3,
                        Price = 22,
                        CategoryId = 2,
                        Keywords = "优雅,主持",
                        YaMoney = 100,
                        Cover = "主持人长裙.jpg",
                    },
                    new()
                    {
                        Id = 10,
                        Checked = 1,
                        Enabled = 1,
                        Name = "舞蹈服",
                        Description = "新疆舞蹈练习裙彝族维族舞练功裙藏族演出服装半身裙成人大摆裙女",
                        Details = " 本款宝贝采用过渡高丝宝精心制作而成，腰部是松紧带设计，腰部空间更灵活。建议身高：160-170cm。",
                        PublisherId = 1,
                        Price = 21,
                        CategoryId = 2,
                        Keywords = "女生,少数民族",
                        YaMoney = 160,
                        Cover = "舞蹈服.jpg",
                    },
                    new()
                    {
                        Id = 11,
                        Checked = 1,
                        Enabled = 1,
                        Name = "拍立得",
                        Description = "Fujifilm富士立拍立得相机instax mini11",
                        Details =
                            "产品名称：Fujifilm/富士 instax mi...，型号: instax mini11，品牌: Fujifilm/富士，相纸尺寸: 86mmx54mm，颜色: 丁香紫，生产企业: 富士，图像尺寸: 62mmx46mm，对焦方式: 固定对焦，是否有近拍镜: 有，近拍镜距离: 30cm，有无闪光灯: 有。",
                        PublisherId = 2,
                        Price = 18,
                        CategoryId = 1,
                        Keywords = "拍照,自拍",
                        YaMoney = 80,
                        Cover = "拍立得.jpg",
                    },
                    new()
                    {
                        Id = 12,
                        Checked = 1,
                        Enabled = 1,
                        Name = "耳机",
                        Description = "B&O Beoplay H95 BO H9 3代舒适版头戴式蓝牙耳机ANC主动降噪三代",
                        Details = "产品名称: B&O Beoplay H95，品牌: B&O，型号: beoplay H95。",
                        PublisherId = 3,
                        Price = 26,
                        CategoryId = 1,
                        Keywords = "炫酷",
                        YaMoney = 300,
                        Cover = "耳机.jpg",
                    },
                    new()
                    {
                        Id = 13,
                        Checked = 1,
                        Enabled = 1,
                        Name = "iPad",
                        Description = "Apple/苹果 10.2 英寸 iPad",
                        Details = "深空灰色，128G。",
                        PublisherId = 2,
                        Price = 30,
                        CategoryId = 1,
                        Keywords = "学习,看网课",
                        YaMoney = 300,
                        Cover = "iPad.jpg",
                    },
                    new()
                    {
                        Id = 14,
                        Checked = 1,
                        Enabled = 1,
                        Name = "华硕笔记本",
                        Description = "华硕vivobook15s i5 i7超薄商务办公轻薄便携 学生女生笔记本电脑",
                        Details =
                            "品牌: Asus/华硕，系列: X550VC，内存容量: 20GB，硬盘容量: 256G固态硬盘，CPU: 英特尔 酷睿 i5-10210U，屏幕尺寸: 15.6英寸，屏幕比例: 16:9，分辨率: 1920x1080，显存容量: 2G,光驱类型: 无光驱。",
                        PublisherId = 1,
                        Price = 50,
                        CategoryId = 1,
                        Keywords = "办公,计算机",
                        YaMoney = 400,
                        Cover = "华硕笔记本.jpg",
                    },
                    new()
                    {
                        Id = 15,
                        Checked = 1,
                        Enabled = 1,
                        Name = "充电宝",
                        Description = "自带线共享超大容量充电宝1000000毫安通用华为oppo苹果vivo闪充",
                        Details =
                            "产品名称: other/其他 1000000M，品牌: other/其他,型号: 1000000m，电池类型: 锂聚合物电池，附加功能: 多U口输出 带LED手电 屏幕数显，是否带屏幕: 带屏幕，外壳材质: 塑料，尺寸: 144x75x24mm，电芯类型: 软包，电池容量: 80000mAh，是否带数据线: 是。",
                        PublisherId = 3,
                        Price = 10,
                        CategoryId = 1,
                        Keywords = "充电,耐用",
                        YaMoney = 120,
                        Cover = "充电宝.jpg",
                    },
                    new ()
                    {
                        Id = 21,
                        Checked = 1,
                        Enabled = 1,
                        Name = "照相机",
                        Description = "Canon佳能PowerShot G7XMarkIII高清卡片机G7X2 g7x G1X3数码相机",
                        Details = "产品名称: Canon/佳能 PowerShot G7 X Mark III，品牌: Canon/佳能，型号: PowerShot G7 X Mark III，像素: 1600万及以上。",
                        PublisherId = 1,
                        Price = 56,
                        CategoryId = 1,
                        Keywords = "摄影,高清",
                        YaMoney = 300,
                        Cover = "照相机.jpg",
                    },
                    new ()
                    {
                        Id = 22,
                        Checked = 1,
                        Enabled = 1,
                        Name = "手表",
                        Description = "Apple/苹果 Apple Watch SE 智能手表iwatch SE 运动多功能心率电话手表商务运动手表",
                        Details = "品牌: Apple/苹果，系列: 深空灰色铝金属表壳配黑色运动型表带，分类: 运动手表，生产企业: 苹果公司，上市时间: 2020-09-20，表壳尺寸: 40mm，连接: GPS GPS + 蜂窝网络。",
                        PublisherId = 2,
                        Price = 60,
                        CategoryId = 1,
                        Keywords = "运动",
                        YaMoney = 260,
                        Cover = "手表.jpg",
                    },
                    new()
                    {
                        Id = 16,
                        Checked = 1,
                        Enabled = 1,
                        Name = "自行车",
                        Description = "凤凰牌折叠山地车自行车成年男女赛车越野24/27速双减震学生单车",
                        Details = "品牌: Phoenix/凤凰，速别: 27速，货号: 路虎折叠山地车。",
                        PublisherId = 2,
                        Price = 20,
                        CategoryId = 4,
                        Keywords = "折叠,运动,交通",
                        YaMoney = 185,
                        Cover = "自行车.jpg",
                    },
                    new()
                    {
                        Id = 17,
                        Checked = 1,
                        Enabled = 1,
                        Name = "电瓶车",
                        Description = "百光三轮车成人电动车",
                        Details = "最高时速: 20km/h(含)-25km/h(不含)，纯电续航: 35km(含)-45km(不含)。",
                        PublisherId = 1,
                        Price = 23,
                        CategoryId = 4,
                        Keywords = "交通",
                        YaMoney = 260,
                        Cover = "电瓶车.jpg",
                    },
                    new()
                    {
                        Id = 18,
                        Checked = 1,
                        Enabled = 1,
                        Name = "高跟鞋",
                        Description = "小高跟鞋女",
                        Details = "品牌: 雨兔，尺码：36码。",
                        PublisherId = 1,
                        Price = 6,
                        CategoryId = 5,
                        Keywords = "优雅,大方",
                        YaMoney = 80,
                        Cover = "高跟鞋.jpg",
                    },
                    new()
                    {
                        Id = 19,
                        Checked = 1,
                        Enabled = 1,
                        Name = "行李箱",
                        Description = "超大容量学生行李箱女超轻拉杆箱30寸",
                        Details = "米白色，含密码锁。",
                        PublisherId = 2,
                        Price = 10,
                        CategoryId = 5,
                        Keywords = "大容量,旅游",
                        YaMoney = 100,
                        Cover = "行李箱.jpg",
                    },
                    new()
                    {
                        Id = 20,
                        Checked = 1,
                        Enabled = 1,
                        Name = "书包",
                        Description = "Gaston Luga功能双肩包",
                        Details = "颜色：黑色，防水程度: 全防水，容纳电脑尺寸: 13英寸.",
                        PublisherId = 3,
                        Price = 9,
                        CategoryId = 5,
                        Keywords = "学习,校园",
                        YaMoney = 180,
                        Cover = "书包.jpg",
                    },
                });
        }
    }
}