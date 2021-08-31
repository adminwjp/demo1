package protos

import (
	"google.golang.org/grpc"
	"log"
	"time"
	pb "shop/protos/impl/service"
	context "context"
)

//c# server go client
func Test()  {
	// Set up a connection to the server.
	conn, err := grpc.Dial("127.0.0.1:5000", grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	defer conn.Close()
	c :=pb.NewStatisticsClient(conn)
	// Contact the server and print out its response.

	ctx, cancel := context.WithTimeout(context.Background(), time.Second)
	defer cancel()
	stream, err := c.BuyerTotal(ctx, &pb.BuyerRequest{BuyerId: 1})
	if err != nil {
		log.Fatalf("could not greet: %v", err)
	}
	log.Printf("Greeting: %s\n pass", stream.String())
}
