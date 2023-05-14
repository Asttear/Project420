using Project420.Shared.Models;

namespace Project420.Client.Services;

public class MockService : IApiService
{
    public async Task<IList<LinkModel>> GetLinksAsync()
    {
        await Task.Delay(1000);
        //return Enumerable.Range(1, 20)
        //                  .Select(i => new LinkModel($"网站 No.{i}", "/", null, "一个很好的网站。"))
        //                  .ToList();
        throw new NotImplementedException();
    }

    public async Task<IList<ArticleMetadata>?> ListArticlesAsync(int count)
    {
        await Task.Delay(1000);
        int remains = 20 - count;
        //return remains > 0
        //    ? Enumerable.Range(count, remains <= 3 ? remains : 3)
        //                .Select(i => new PostListItemModel(i.ToString(), $"一篇好文章 No.{i + 1}", "佚名", DateTimeOffset.Now))
        //                .ToList()
        //    : null;
        return null;
    }

    public async Task<ArticleModel> GetArticleAsync(string id)
    {
        await Task.Delay(1000);
        return new ArticleModel()
        {
            Title = "谁是布洛芬的第一个服用者？",
            Source = "中国科学报",
            CreatedTime = DateTime.Now,
            HtmlContent =
                    """
                    <div id="content1" style=" padding:15px; text-align: left; line-height: 24px;
                                                                word-wrap: break-word;word-break:break-all" class="f14">
                        <br>
                        <p align="center">
                            <img src="/image/post/p1.jpg">
                        </p>
                        <p style="text-indent: 2em; text-align: center;">
                            英国皇家化学会表彰布洛芬研发者斯图尔特·亚当斯等的蓝色牌匾。<strong>图片来源：invent.org</strong>
                        </p>
                        <p style="text-indent: 2em; text-align: center;">
                            <img src="/image/post/p2.jpg">
                        </p>
                        <p style="text-indent: 2em; text-align: center;">
                            斯图尔特·亚当斯
                        </p>
                        <p style="text-indent: 2em; text-align: center;">
                            <img src="/image/post/p3.jpg">
                        </p>
                        <p style="text-indent: 2em; text-align: center;">
                            约翰·尼科尔森
                        </p>
                        <p style="text-indent:2em">
                            <strong>■张田勘</strong>
                        </p>
                        <p style="text-indent:2em">
                            2022年12月，新冠病毒感染人数激增，消炎止痛药布洛芬顿时成为抢手货。
                        </p>
                        <p style="text-indent:2em">
                            如果布洛芬的发明人斯图尔特·亚当斯还活着，他今年正好是100岁。4年前的2019年1月30日，亚当斯在英国皇后医学中心去世。布洛芬问世60年来，一直是全世界人民最常用的、物美价廉的退烧镇痛消炎药。
                        </p>
                        <p style="text-indent:2em">
                            但这位头痛了也会和普通人一样去药店买布洛芬的发明人，并不会知道在他去世后的新冠病毒肆虐期间，布洛芬会成为“新晋网红”。
                        </p>
                        <p style="text-indent:2em">
                            <strong>药物止痛 四分天下</strong>
                        </p>
                        <p style="text-indent:2em">
                            人的一生都会在不同阶段生病，有病就会身体疼痛，甚至无病也有疼痛。但疼痛的程度有差异，可简单分为轻度疼痛、中度疼痛和重度疼痛。如果有镇痛药，人就会好受一些，对疾病的耐受也强一些，能够更好地与病魔斗争。
                        </p>
                        <p style="text-indent:2em">
                            止痛药有许多种类，迄今，人类发明的止痛药大致有几类。一是非甾体类，作用机理是抑制前列腺素的合成，布洛芬和对乙酰氨基酚就属于这一类。二是中枢性止痛药，通过麻醉中枢神经起到止痛的作用，常见的有吗啡、曲马多等。三是解痉止痛药，主要通过解除肌肉平滑肌的痉挛而缓解疼痛，主要有山莨菪碱、颠茄片等。四是抗焦虑类止痛药，如安定、艾司唑仑等。
                        </p>
                        <p style="text-indent:2em">
                            人在患病时，细胞组织受损会产生炎症。疼痛的发生与炎症反应有很大关系。炎症的主要表现是红、肿、热、痛和功能障碍。红、肿主要是由于血管扩张和通透性增加导致，热、痛主要是由炎性介质如前列腺素、5-羟色胺、缓激肽等引发。布洛芬就是通过炎症发作时抑制前列腺素等致痛物质的产生过程，达到止痛效果。
                        </p>
                        <p style="text-indent:2em">
                            布洛芬对慢性钝痛、炎症或组织损伤所致疼痛、手术后疼痛有效，但对急性锐痛、各种严重创伤疼痛、内脏平滑肌疼痛无效。现在，在世界卫生组织的药物清单中，布洛芬适用于发烧、轻度及中度疼痛（包含外科手术后疼痛）、经痛、骨关节炎、牙痛、头痛，以及肾结石造成的疼痛。同时也适用于幼年特发性关节炎、类风湿性关节炎、心包炎和动脉导管未闭所致的疼痛。
                        </p>
                        <p style="text-indent:2em">
                            不过，布洛芬的研发和问世经历了漫长的时间和一波三折的过程。
                        </p>
                        <p style="text-indent:2em">
                            <strong>有心栽花 十年一剑</strong>
                        </p>
                        <p style="text-indent:2em">
                            布洛芬的发明者是斯图尔特·亚当斯，但由于他是英国博姿公司（Boots）的终身员工，因此，这个药物的发明和专利属于职务行为，发明者和专利拥有者属于博姿公司和亚当斯。
                        </p>
                        <p style="text-indent:2em">
                            1923年4月16日，英国北安普顿郡拜菲尔德镇一位铁路职工的家庭又添一丁，他就是亚当斯，在他之前家里已经有两个哥哥一个姐姐，此后几年亚当斯又有了一个弟弟。5个兄弟姐妹中，只有亚当斯日后成就杰出。
                        </p>
                        <p style="text-indent:2em">
                            由于家境贫困，亚当斯启蒙上学时就读于拜菲尔德镇立学校。1933年，亚当斯随父母迁至南约克郡的唐卡斯特镇，在唐卡斯特文法学校就读。1937年，亚当斯又升学到马齐语法学校，但终因家里无力支付学费而于亚当斯16岁（1939年）时辍学。
                        </p>
                        <p style="text-indent:2em">
                            此后，亚当斯在马齐的博姿连锁店（药店）做了3年学徒，并成为一名药剂师。这个过程让亚当斯对药物学产生了兴趣。一方面是亚当斯愿意学习，另一方面是他的勤奋工作赢得了博姿公司的信任，后者资助他到英国诺丁汉大学学习药学。1945年亚当斯获得医药学本科学历。
                        </p>
                        <p style="text-indent:2em">
                            毕业后，亚当斯再回博姿工作，并参与公司研发青霉素的研究。5年后，亚当斯获得了英国皇家医药学会提供的600英镑奖学金，资助他攻读利兹大学的药理学博士学位。1952年，亚当斯完成学业，重回博姿公司的研发小组。在博士毕业后的1953年，亚当斯才转向止痛药的研究。因此，布洛芬的研究并非完全是“无心插柳”，部分是亚当斯和其公司的“有心栽花”之作。
                        </p>
                        <p style="text-indent:2em">
                            当时，亚当斯和公司开始研究类风湿性关节炎的治疗方法。这种疾病会给患者带来剧烈疼痛，当时能缓解这种疼痛的主要药物只有皮质类固醇和高剂量的阿司匹林，但是，这些药物都有难以忍受的副作用。
                        </p>
                        <p style="text-indent:2em">
                            <strong>宿醉头痛 以身试药</strong>
                        </p>
                        <p style="text-indent:2em">
                            虽然布洛芬是亚当斯和公司团队的有意之作，但其间也经历了许多无意之举，而且时间漫长。这也证明，一种新药或疫苗的研发需要“两个十”以上，即10年以上的时间、10亿美元以上的经费。
                        </p>
                        <p style="text-indent:2em">
                            为了避开高剂量阿司匹林的副作用，亚当斯转而研发其他能止痛的化学物质。1953年后，亚当斯找到了两位有志者协助研究，一位是化学家约翰·尼科尔森博士，另一位是技术员科林·伯罗斯。
                        </p>
                        <p style="text-indent:2em">
                            研究团队在诺丁汉郊区的6年中夜以继日地测试了600多种化合物，其中只有4种走到了临床试验阶段，但都因为在试验中出现了较大副作用而功亏一篑。不过，20世纪50年代末，亚当斯团队发现，此前的化合物出现较大的副作用，是因为它们会被人体的各个组织大量吸收。因此，只有让药物量既小又能起作用，才能避免副作用。
                        </p>
                        <p style="text-indent:2em">
                            于是，他们把关注力从乙酸调整到丙酸。在他们开始研究一个名叫苯基丙酸家族的化合物时，亚当斯团队发现了一种叫对异丁基苯异丙酸的化合物，不仅能止痛，而且副作用小。在动物和人身上进行了成百上千次试验后，他们确认其药效是阿司匹林的3倍多，副作用也小得多，而且还能治发烧。
                        </p>
                        <p style="text-indent:2em">
                            亚当斯一直认为，在他人服用药物之前，自己应该是药物的第一个服用者。因此在药物申请之前，亚当斯坚持先在自己身上试验以最后确认效果。
                        </p>
                        <p style="text-indent:2em">
                            在对异丁基苯异丙酸的若干试验疗效中，最主要的是治疗头疼。恰好有一天早上亚当斯因为宿醉头疼欲裂，当天又有一场重要的学术演讲，他毫不犹豫服下600毫克刚合成好的药物。结果让他惊讶，他发现自己的脑子变得很清醒。这也是他以身试药的又一次实践，此前他已经以身试药过多次。
                        </p>
                        <p style="text-indent:2em">
                            1962年，公司把该药的商品名定为布洛芬（ibuprofen），正式向英国政府申请上市并申请了专利。在等待了7年之后，1969年，布洛芬作为一种处方药获得到了批准，被列为风湿性疾病的处方药。又过了5年，美国也批准布洛芬以处方药销售。
                        </p>
                        <p style="text-indent:2em">
                            对此，亚当斯极为自豪。他说，能让布洛芬获得英国和美国这两个世界上最严格的药品监管国家的认可，是他最理想的目标。
                        </p>
                        <p style="text-indent:2em">
                            1980年，布洛芬成为英国排名第一的抗炎药。1983年，由于患者在使用布洛芬时一直处于一种相对“安全状态”，该药被英国批准为非处方药。1984年，美国也批准该药为非处方药。于是，就有了后来的布洛芬风靡全球，直到今天在应对新冠疫情时成为明星药物。
                        </p>
                        <p style="text-indent:2em">
                            <strong>价廉物美 伟大发现</strong>
                        </p>
                        <p style="text-indent:2em">
                            1983年，亚当斯从博姿公司的药理科学系主任的职位上退休。2019年1月30日，亚当斯在英国皇后医学中心去世。从他16岁起为公司打工算起，亚当斯一共为博姿公司工作了近46年。亚当斯和全家一直住在诺丁汉市郊外。每当他头痛时，他会像普通人一样去药店购买布洛芬。
                        </p>
                        <p style="text-indent:2em">
                            由于是博姿公司的终身雇员和高层人员，亚当斯从公司获得的福利待遇极为优厚，而且也有股份，这或许是亚当斯并未因为发明布洛芬而在专利和回报方面与公司发生矛盾的原因之一。
                        </p>
                        <p style="text-indent:2em">
                            说句题外话，科学史上有不少专利发明者与公司发生过难以调和的矛盾。因发明蓝色发光二极管（LED）而获2014年诺贝尔物理学奖（获奖者之一）的美籍日裔科学家中村修二，于2001年将自己的雇主日亚化学工业公司告上法庭，讨要发明的收益。
                        </p>
                        <p style="text-indent:2em">
                            中村修二发明LED后，公司只奖励他2万日元（约200美元、人民币1250元）。2004年，东京一家法院判决日亚化工向中村修二支付200亿日元（1.83亿美元）补偿金。但在二审时，这一赔偿被降至8.4亿日元（631万美元）。尽管如此，也反映了日本公司与职员之间利益的巨大不平等。事实上这些发明是职务发明还是个人发明，难以区分。
                        </p>
                        <p style="text-indent:2em">
                            现在，布洛芬的专利期已过，布洛芬可在各国生产，而且名字不尽相同。尽管全世界有数不清的布洛芬品种，但主要成分一直未变，而且人们都喜爱这款物美价廉的退烧镇痛消炎药。
                        </p>
                        <p style="text-indent:2em">
                            由于发明了布洛芬，亚当斯也获得了极高荣誉。2013年9月，英国皇家化学会表彰布洛芬的研发，将与研发有关的两处建筑挂上蓝色牌匾标识。在诺丁汉生物科技园（原实验室所在地）的牌匾上写道：表彰斯图尔特·亚当斯博士和约翰·尼科尔森博士在此完成的研究创举；从博姿公司的研究部门，到无数患者的痛苦缓解，只感谢这一伟大的发现。
                        </p>
                        <p style="text-indent:2em">
                            <em>参考文献：</em>
                        </p>
                        <p style="text-indent:2em">
                            <em>1.https://doi.org/10.1016/j.tips.2011.10.007</em>
                        </p>
                        <p style="text-indent:2em">
                            <em>2.https://doi.org/10.3109/09537104.2011.632032</em>
                        </p>
                        <p style="text-indent:2em">
                            <em>3.Dr Stewart Adams: I tested ibuprofen on my hangover.the telegraph</em>
                        </p>
                        <p style="text-indent:2em">
                            <em>4.Ibuprofen：a critical bibliographic review</em>
                        </p>
                        <p style="text-indent:2em">
                            <strong>链接</strong>
                        </p>
                        <p style="text-indent: 2em; text-align: center;">
                            <span style="font-size:20px;"><strong>布洛芬并非万能止痛药</strong></span>
                        </p>
                        <p style="text-indent:2em">
                            尽管布洛芬能缓解很多疼痛，但并非是万能止痛药，有一些疼痛是不能使用的，其中最应禁忌的是心脏疾病引起的疼痛。心脏疾病发作时可能会导致心源性牙痛、后背疼痛、肩膀疼痛、胸部疼痛，此时应尽快就医，不可自行服用布洛芬，以免耽误最佳治疗时机。
                        </p>
                        <p style="text-indent:2em">
                            布洛芬的非万能性也体现在一些服用禁忌上。孕妇和哺乳期妇女禁用，因为布洛芬可能通过胎盘对胎儿造成损害，怀孕20周以上使用可致胎儿肾脏损害，同时增加羊水不足风险，此外布洛芬能够进入乳汁，影响婴儿健康。备孕女性也要禁服布洛芬，因为该药会延迟或阻止排卵，引起可逆性不孕。
                        </p>
                        <p style="text-indent:2em">
                            布洛芬的副作用小，但并不意味着没有副作用，其副作用包括恶心、呕吐、胃烧灼感、轻度消化不良、头痛、头晕、耳鸣、视物模糊、精神紧张、嗜睡、下肢水肿或体重骤增等。
                        </p>
                        <p style="text-indent:2em">
                            另外，使用布洛芬也有一些禁忌。比如用于镇痛不要超过5天，用于解热不要超过3天；由于布洛芬可能抑制凝血功能，用药后可能更容易出血，因此用药期间要使用软毛牙刷和电动剃须刀等；在服用布洛芬的同时不要服用其他含有解热镇痛成分的药物等。
                        </p>
                        <div style="text-indent:2em;color: #587c19">
                            《中国科学报》 (2023-01-06 第4版 文化)
                        </div>
                        <div style="border-bottom: solid 0px #bfc89d; vertical-align: bottom; width: 100%;
                    height: 19px">
                    </div>
                    </div>
                    """
        };
    }

    public Task<IList<CommentModel>?> GetCommentsAsync(string postId, int count)
    {
        throw new NotImplementedException();
    }

    public Task AddCommentAsync(string articleId, string comment)
    {
        throw new NotImplementedException();
    }

    public Task<IList<DiscussionMetadata>?> ListDiscussionsAsync(int count)
    {
        throw new NotImplementedException();
    }

    public Task<DiscussionModel> GetDiscussionAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task AddDiscussionAsync(string title, string htmlContent)
    {
        throw new NotImplementedException();
    }

    public Task<ProfileModel> GetProfileAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProfileModel> GetProfileAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<ProfileModel> ModifyProfileAsync(ProfileModel model)
    {
        throw new NotImplementedException();
    }

    public Task<IList<CaseMetadata>?> ListCasesAsync(int count)
    {
        throw new NotImplementedException();
    }

    public Task<CaseModel> GetCaseAsync(string id)
    {
        throw new NotImplementedException();
    }
}