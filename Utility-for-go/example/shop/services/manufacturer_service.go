package services

import "news/shop/models"

func AddManufacturer(manufacturer models.Manufacturer) bool {
	manufacturer.Id = Id.GetIdByStruct(manufacturer)
	res,_:= add(manufacturer)
	return res
}

func ManufacturerList(manufacturer models.Manufacturer, page int, size int) ([]*models.Manufacturer, int64) {
	var manufacturers []*models.Manufacturer
	data, count,_ := selectList(manufacturer, page, size, manufacturers)
	return data.([]*models.Manufacturer), count
}



