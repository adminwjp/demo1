package news

// https://www.sojson.com/json/json2go.html
type NewsCatalogs struct {
	Catalogs []*NewsCatalog `json:"catalogs"`
}

type NewsCatalog struct {
	ID      string `json:"id"`
	Name    string `json:"name"`
	Href    string `json:"href"`
	Newsid  int    `json:"newsid"`
	Soucrce string `json:"soucrce"`
}

func (NewsCatalog) TableName() string {
	//实现TableName接口，以达到结构体和表对应，如果不实现该接口，并未设置全局表名禁用复数，gorm会自动扩展表名为articles（结构体+s）
	return "t_news_catalog"
}

type NewsItems struct {
	Items []*NewsItem `json:"items"`
}

type NewsItem struct {
	ID            int    `json:"id"`
	PostID        string `json:"post_id"`
	Title         string `json:"title"`
	AuthorName    string `json:"author_name"`
	Cover         string `json:"cover"`
	Img           string `json:"img"`
	Newsid        int    `json:"newsid"`
	CatalogId     string `json:"catalog_id"`
	PublishedAt   string `json:"published_at"`
	Published     int64  `json:"published"`
	CommentsCount int    `json:"comments_count"`
	Content       string `json:"content"`
	Href          string `json:"href"`
	Soucrce       string `json:"soucrce"`
	LikeCount     int    `json:"like_count"`
	Pic           string `json:"pic"`
	Info          string `json:"info"`
	Video         string `json:"video"`
	Incentive     string `json:"incentive"`
	ReplayCount   int    `json:"replay_count"`
	Flag          string `json:"flag"`
	AddDate       int64  `json:"add_date"`
}

func (NewsItem) TableName() string {
	//实现TableName接口，以达到结构体和表对应，如果不实现该接口，并未设置全局表名禁用复数，gorm会自动扩展表名为articles（结构体+s）
	return "t_news_item"
}
