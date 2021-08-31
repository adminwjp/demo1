package services

type Event interface {


}

type EventBus interface {
	Subscribe()

	Publish(data interface{})
}
